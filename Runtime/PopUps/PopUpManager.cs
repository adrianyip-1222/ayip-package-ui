using System.Collections.Generic;
using AYip.UI.Events;
using UnityEngine;

namespace AYip.UI.PopUps
{
    /// <summary>
    /// A manager for pop-ups that handles showing and queuing them based on their models.
    /// This manager allows for showing pop-ups immediately if no other pop-up is currently active,
    /// or queuing them for later display if a pop-up is already showing.
    /// </summary>
    public abstract class PopUpManager<TPrefabKey, TPopUp, TModel> : ViewManager<TPrefabKey, TPopUp, Queue<IQueueable>, IQueueable, TModel>
        where TPopUp : IPopUp<TPrefabKey, TModel>
        where TModel : IPopUpModel<TPrefabKey>
    {
        protected PopUpManager(
            RectTransform defaultCanvasRoot,
            IHandler viewStateEventHandler,
            IViewFactory viewFactory)
            : base(defaultCanvasRoot, viewStateEventHandler, viewFactory)
        {
            ViewContainer = new Queue<IQueueable>();
        }

        public override int StackCounts => ViewContainer.Count;

        /// <summary>
        /// Attempts to show the pop-up by its model immediately or enqueue it if there is a pop-up already showing (show and forget).
        /// </summary>
        /// <param name="model">The model of the pop-up</param>
        /// <param name="overrideCanvasRoot">The canvas root to override the default one.</param>
        /// <returns>If the pop-up was shown immediately.</returns>
        public bool TryShowOrEnqueue(TModel model, RectTransform overrideCanvasRoot = null)
        {
            return TryShowOrEnqueue(model, out _, overrideCanvasRoot);
        }

        /// <summary>
        /// Attempts to show the pop-up by its model immediately or enqueue it if there is a pop-up already showing.
        /// </summary>
        /// <param name="model">The model of the pop-up</param>
        /// <param name="createdPopUp">The created pop-up if it can be shown immediately, otherwise default.</param>
        /// <param name="overrideCanvasRoot">The canvas root to override the default one.</param>
        /// <returns>If the pop-up was shown immediately.</returns>
        public bool TryShowOrEnqueue(TModel model, out IPopUp createdPopUp, RectTransform overrideCanvasRoot = null)
        {
            createdPopUp = null;
            if (!TryShowViewBy(model, out var view, overrideCanvasRoot))
            {
                return false;
            }

            createdPopUp = (IPopUp) view;
            return true;
        }

        /// <summary>
        /// Force show a pop-up. Typically used for critical pop-ups that must be shown immediately.
        /// The pop-up won't be enqueued, which needs manually handling the lifecycle.
        /// </summary>
        /// <param name="model">The model of the pop-up</param>
        public T ForceShow<T>(TModel model) where T: TPopUp
        {
            var popUp = CreateViewBy(model);
            return (T) popUp;
        }
        
        protected override bool TryShowViewBy(TModel model, out TPopUp createdPopUp, RectTransform overrideCanvasRoot = null)
        {
            createdPopUp = default;
			
            // If there is a pop-up showing, enqueue the model.
            if (CurrentView != null)
            {
                AddToCollection(model);
                return false;
            }

            var baseView = CreateViewBy(model, overrideCanvasRoot);
            CurrentView = baseView;
            return true;
        }

        protected override void AddToCollection(IQueueable viewOrModel)
        {
            ViewContainer.Enqueue(viewOrModel);
        }

        protected override bool TryRetrieveNextViewOrModel(out IQueueable nextViewOrModel)
        {
            nextViewOrModel = null;
            
            if (ViewContainer.Count == 0)
            {
                return false;
            }
            
            nextViewOrModel = ViewContainer.Dequeue();
            return true;
        }
    }
}