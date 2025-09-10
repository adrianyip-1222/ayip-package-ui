using System.Collections;
using System.Collections.Generic;
using AYip.Foundation;
using AYip.UI.Events;
using UnityEngine;

namespace AYip.UI
{
	/// <summary>
	/// Responsible for managing the windows.
	/// </summary>
	public abstract class WindowManager : DisposableBase
	{
		protected WindowManager(RectTransform canvasRoot, IWindowStateEventHandler windowStateEventHandler, IWindowFactory windowFactory)
		{
			CanvasRoot = canvasRoot;
			WindowStateEventHandler = windowStateEventHandler;
			WindowFactory = windowFactory;
		}
		
		/// <summary>
		/// The currently active window.
		/// </summary>
		public IWindow CurrentWindow { get; protected set; }

		/// <summary>
		/// The collection of windows managed by this manager.
		/// </summary>
		protected IEnumerable WindowCollection { get; set; }
		
		/// <summary>
		/// The canvas root to instantiate the windows to.
		/// </summary>
		protected RectTransform CanvasRoot { get; }

		/// <summary>
		/// The handler to manage state events publish and subscribe.
		/// </summary>
		protected IWindowStateEventHandler WindowStateEventHandler { get; }

		/// <summary>
		/// The factory to create windows in DI manner.
		/// </summary>
		protected IWindowFactory WindowFactory { get; }
	}

	/// <summary>
	/// A generic window manager that manages windows and modals of specific types.
	/// </summary>
	/// <typeparam name="TPrefabKey">The prefab key type used to get the prefab from the database.</typeparam>
	/// <typeparam name="TWindow">The type of window to manage.</typeparam>
	/// <typeparam name="TIEnumerable">The type of the enumerable collection that holds the windows and modals.</typeparam>
	/// <typeparam name="TPushable">The type of the windows and modals that can be pushed to the collection.</typeparam>
	/// <typeparam name="TModal">The type of modal to create a window.</typeparam>
	public abstract class WindowManager<TPrefabKey, TWindow, TIEnumerable, TPushable, TModal> : WindowManager
		where TWindow : IWindow<TPrefabKey, TWindow, TModal>
		where TIEnumerable : IEnumerable<TPushable>, new()
		where TModal : IWindowModal<TPrefabKey>, TPushable
	{
		protected WindowManager(RectTransform canvasRoot, IWindowStateEventHandler windowStateEventHandler, IWindowFactory windowFactory) 
			: base(canvasRoot, windowStateEventHandler, windowFactory)
		{ }

		public new TWindow CurrentWindow
		{
			get => (TWindow)base.CurrentWindow;
			protected set => base.CurrentWindow = value;
		}

		protected new TIEnumerable WindowCollection
		{
			get => (TIEnumerable)base.WindowCollection;
			set => base.WindowCollection = value;
		}

		/// <summary>
		/// Attempts to show the window by its modal immediately or adding it for later user if there is a window already showing.
		/// </summary>
		/// <param name="modal">The modal to show the window</param>
		/// <param name="createdWindow">The created window, can be null if there is a window showing.</param>
		/// <returns>If the window was shown immediately.</returns>
		protected abstract bool TryShowWindowBy(TModal modal, out TWindow createdWindow);

		protected TWindow CreateWindowBy(TModal modal)
		{
			// Create the window with the model.
			var baseWindow = WindowFactory.Create(modal, CanvasRoot);
			
			// Prepare the event and subscribe to it.
			var eventSubscriptionDto = new EventSubscriptionDTO(baseWindow, WindowState.Closed, OnWindowClosed);
			WindowStateEventHandler.Subscribe(eventSubscriptionDto);
			
			return (TWindow) baseWindow;
		}

		protected abstract void AddToCollection(TPushable windowOrModal);

		/// <summary>
		/// Called when the current window is closed.
		/// </summary>
		protected virtual void OnWindowClosed(IWindow window)
		{
			// Prepare the event and subscribe to it.
			WindowStateEventHandler.Unsubscribe(CurrentWindow);
			
			// Destroy the window instance.
			Object.Destroy(window.GameObject);
			CurrentWindow = default;
			
			if (!TryRetrieveNextWindowOrModal(out var nextWindowOrModal))
			{
				return;
			}

			switch (nextWindowOrModal)
			{
				case TModal nextModal:
					TryShowWindowBy(nextModal, out _);
					return;
				
				case TWindow nextWindow:
					nextWindow.Visibility = true;
					WindowStateEventHandler.Subscribe(new EventSubscriptionDTO(window, WindowState.Closed, OnWindowClosed));
					
					CurrentWindow = nextWindow;
					return;
				
				default:
					throw new System.Exception ($"Window {nextWindowOrModal.GetType().FullName} is not type of {nameof(IWindow)} or {typeof(TModal)}.");
			}
		}

		protected abstract bool TryRetrieveNextWindowOrModal(out TPushable nextWindowOrModal);
	}
}