using System.Collections;
using System.Collections.Generic;
using AYip.Foundation;
using AYip.UI.Events;
using UnityEngine;

namespace AYip.UI
{
	/// <summary>
	/// Responsible for managing the views.
	/// </summary>
	public abstract class ViewManager : DisposableBase
	{
		protected ViewManager(RectTransform defaultCanvasRoot, IHandler viewStateEventHandler, IViewFactory viewFactory)
		{
			DefaultCanvasRoot = defaultCanvasRoot;
			ViewStateEventHandler = viewStateEventHandler;
			ViewFactory = viewFactory;
		}
		
		/// <summary>
		/// The currently active view.
		/// </summary>
		public IView CurrentView { get; protected set; }
		
		/// <summary>
		/// The count of views managed by this manager.
		/// </summary>
		public abstract int StackCounts { get; }

		/// <summary>
		/// The collection of views managed by this manager.
		/// </summary>
		protected IEnumerable ViewCollection { get; set; }
		
		/// <summary>
		/// The canvas root to instantiate the views to.
		/// </summary>
		protected RectTransform DefaultCanvasRoot { get; }

		/// <summary>
		/// The handler to manage state events publish and subscribe.
		/// </summary>
		protected IHandler ViewStateEventHandler { get; }

		/// <summary>
		/// The factory to create views in DI manner.
		/// </summary>
		protected IViewFactory ViewFactory { get; }
	}

	/// <summary>
	/// A generic view manager that manages views and models of specific types.
	/// </summary>
	/// <typeparam name="TPrefabKey">The prefab key type used to get the prefab from the database.</typeparam>
	/// <typeparam name="TView">The type of view to manage.</typeparam>
	/// <typeparam name="TIEnumerable">The type of the enumerable collection that holds the views and models.</typeparam>
	/// <typeparam name="TPushable">The type of the views and models that can be pushed to the collection.</typeparam>
	/// <typeparam name="TModel">The type of model to create a view.</typeparam>
	public abstract class ViewManager<TPrefabKey, TView, TIEnumerable, TPushable, TModel> : ViewManager
		where TView : IView<TPrefabKey, TModel>
		where TIEnumerable : IEnumerable<TPushable>, new()
		where TModel : IViewModel<TPrefabKey>, TPushable
	{
		protected ViewManager(RectTransform defaultCanvasRoot, IHandler viewStateEventHandler, IViewFactory viewFactory) 
			: base(defaultCanvasRoot, viewStateEventHandler, viewFactory)
		{ }

		public new TView CurrentView
		{
			get => (TView)base.CurrentView;
			protected set => base.CurrentView = value;
		}

		protected new TIEnumerable ViewContainer
		{
			get => (TIEnumerable)base.ViewCollection;
			set => base.ViewCollection = value;
		}

		/// <summary>
		/// Attempts to show the view by its model immediately or adding it for later user if there is a view already showing.
		/// </summary>
		/// <param name="model">The model to show the view</param>
		/// <param name="createdView">The created view, can be null if there is a view showing.</param>
		/// <param name="overrideCanvasRoot">The canvas root to override the default one.</param>
		/// <returns>If the view was shown immediately.</returns>
		protected abstract bool TryShowViewBy(TModel model, out TView createdView, RectTransform overrideCanvasRoot = null);

		protected TView CreateViewBy(TModel model, RectTransform overrideCanvasRoot = null)
		{
			// Create the view with the model.
			var baseView = ViewFactory.TrySpawnView(model, overrideCanvasRoot? overrideCanvasRoot : DefaultCanvasRoot);
			
			// Prepare the event and subscribe to it.
			ViewStateEventHandler.Subscribe(baseView, ViewState.Closed, OnViewClosed);
			
			return (TView) baseView;
		}

		protected abstract void AddToCollection(TPushable viewOrModel);

		/// <summary>
		/// Called when the current view is closed.
		/// </summary>
		protected virtual void OnViewClosed(IView targetView, ViewState targetState)
		{
			// Prepare the event and subscribe to it.
			ViewStateEventHandler.Unsubscribe(CurrentView);
			
			// Destroy the view instance.
			Object.Destroy(targetView.GameObject);
			CurrentView = default;
			
			if (!TryRetrieveNextViewOrModel(out var nextViewOrModel))
			{
				return;
			}

			switch (nextViewOrModel)
			{
				case TModel nextModel:
					TryShowViewBy(nextModel, out var createdView);
					CurrentView = createdView;
					return;
				
				case TView nextView:
					ViewStateEventHandler.Subscribe(targetView, ViewState.Closed, OnViewClosed);
					
					CurrentView = nextView;
					return;
				
				default:
					throw new System.Exception ($"view {nextViewOrModel.GetType().FullName} is not type of {nameof(IView)} or {typeof(TModel)}.");
			}
		}

		protected abstract bool TryRetrieveNextViewOrModel(out TPushable nextViewOrModel);
	}
}