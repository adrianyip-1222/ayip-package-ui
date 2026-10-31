using System.Collections;
using System.Collections.Generic;
using AYip.Foundation;
using AYip.UI.Events;

namespace AYip.UI
{
	/// <summary>
	/// Responsible for managing the views.
	/// </summary>
	public abstract class ViewManager : DisposableBase
	{
		protected ViewManager(IHandler viewStateEventHandler)
		{
			ViewStateEventHandler = viewStateEventHandler;
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
		/// The handler to manage state events publish and subscribe.
		/// </summary>
		protected IHandler ViewStateEventHandler { get; }
	}

	/// <summary>
	/// A generic view manager that manages views and models of specific types.
	/// </summary>
	/// <typeparam name="TRoot">The parent the views are spawned under, e.g. RectTransform for uGUI or VisualElement for UI Toolkit.</typeparam>
	/// <typeparam name="TPrefabKey">The prefab key type used to get the prefab from the database.</typeparam>
	/// <typeparam name="TView">The type of view to manage.</typeparam>
	/// <typeparam name="TIEnumerable">The type of the enumerable collection that holds the views and models.</typeparam>
	/// <typeparam name="TPushable">The type of the views and models that can be pushed to the collection.</typeparam>
	/// <typeparam name="TModel">The type of model to create a view.</typeparam>
	public abstract class ViewManager<TRoot, TPrefabKey, TView, TIEnumerable, TPushable, TModel> : ViewManager
		where TRoot : class
		where TView : IView<TPrefabKey, TModel>
		where TIEnumerable : IEnumerable<TPushable>, new()
		where TModel : IViewModel<TPrefabKey>, TPushable
	{
		protected ViewManager(TRoot defaultRoot, IHandler viewStateEventHandler, IViewFactory<TRoot> viewFactory)
			: base(viewStateEventHandler)
		{
			DefaultRoot = defaultRoot;
			ViewFactory = viewFactory;
		}

		public new TView CurrentView
		{
			get => (TView)base.CurrentView;
			protected set => base.CurrentView = value;
		}

		protected TIEnumerable ViewContainer
		{
			get => (TIEnumerable)ViewCollection;
			set => ViewCollection = value;
		}

		/// <summary>
		/// The root to spawn the views under when no override is given.
		/// </summary>
		protected TRoot DefaultRoot { get; }

		/// <summary>
		/// The factory that spawns and despawns the views.
		/// </summary>
		protected IViewFactory<TRoot> ViewFactory { get; }

		/// <summary>
		/// Attempts to show the view by its model immediately or adding it for later user if there is a view already showing.
		/// </summary>
		/// <param name="model">The model to show the view</param>
		/// <param name="createdView">The created view, can be null if there is a view showing.</param>
		/// <param name="overrideRoot">The root to override the default one.</param>
		/// <returns>If the view was shown immediately.</returns>
		protected abstract bool TryShowViewBy(TModel model, out TView createdView, TRoot overrideRoot = null);

		protected TView CreateViewBy(TModel model, TRoot overrideRoot = null)
		{
			// Create the view with the model.
			var baseView = ViewFactory.TrySpawnView(model, overrideRoot ?? DefaultRoot);

			// Prepare the event and subscribe to it.
			ViewStateEventHandler.Subscribe(baseView, ViewState.Closed, OnViewClosed);

			return (TView) baseView;
		}

		/// <summary>
		/// Stop listening to the view and tear it down through the factory.
		/// </summary>
		protected void DespawnView(IView view)
		{
			ViewStateEventHandler.Unsubscribe(view);
			ViewFactory.Despawn(view);
		}

		protected abstract void AddToCollection(TPushable viewOrModel);

		/// <summary>
		/// Called when a view spawned by this manager is closed.
		/// </summary>
		protected virtual void OnViewClosed(IView targetView, ViewState targetState)
		{
			DespawnView(targetView);

			// A view outside the flow, e.g. a force-shown pop-up, leaves the current view untouched.
			if (!targetView.Equals(base.CurrentView))
			{
				return;
			}

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
					// A view is unsubscribed while it waits in the collection, so it listens again once it is current.
					ViewStateEventHandler.Subscribe(nextView, ViewState.Closed, OnViewClosed);

					CurrentView = nextView;
					return;

				default:
					throw new System.Exception ($"view {nextViewOrModel.GetType().FullName} is not type of {nameof(IView)} or {typeof(TModel)}.");
			}
		}

		protected abstract bool TryRetrieveNextViewOrModel(out TPushable nextViewOrModel);
	}
}