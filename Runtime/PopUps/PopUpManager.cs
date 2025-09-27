using System.Collections.Generic;
using AYip.UI.Events;
using UnityEngine;

namespace AYip.UI.PopUps
{
    /// <summary>
    /// A manager for pop-ups that handles showing and queuing them based on their modals.
    /// This manager allows for showing pop-ups immediately if no other pop-up is currently active,
    /// or queuing them for later display if a pop-up is already showing.
    /// </summary>
    public abstract class PopUpManager<TPrefabKey, TPopUp, TModal> : WindowManager<TPrefabKey, TPopUp, Queue<IQueueable>, IQueueable, TModal>
        where TPopUp : IPopUp<TPrefabKey, TModal>
        where TModal : IPopUpModal<TPrefabKey>
    {
        protected PopUpManager(
            RectTransform defaultCanvasRoot,
            IWindowStateEventHandler windowStateEventHandler,
            IWindowFactory windowFactory)
            : base(defaultCanvasRoot, windowStateEventHandler, windowFactory)
        {
            WindowContainer = new Queue<IQueueable>();
        }

        public override int StackCounts => WindowContainer.Count;

        /// <summary>
        /// Attempts to show the pop-up by its modal immediately or enqueue it if there is a pop-up already showing (show and forget).
        /// </summary>
        /// <param name="modal">The modal of the pop-up</param>
        /// <param name="overrideCanvasRoot">The canvas root to override the default one.</param>
        /// <returns>If the pop-up was shown immediately.</returns>
        public bool TryShowOrEnqueue(TModal modal, RectTransform overrideCanvasRoot = null)
        {
            return TryShowOrEnqueue(modal, out _, overrideCanvasRoot);
        }

        /// <summary>
        /// Attempts to show the pop-up by its modal immediately or enqueue it if there is a pop-up already showing.
        /// </summary>
        /// <param name="modal">The modal of the pop-up</param>
        /// <param name="createdPopUp">The created pop-up if it can be shown immediately, otherwise default.</param>
        /// <param name="overrideCanvasRoot">The canvas root to override the default one.</param>
        /// <returns>If the pop-up was shown immediately.</returns>
        public bool TryShowOrEnqueue(TModal modal, out IPopUp createdPopUp, RectTransform overrideCanvasRoot = null)
        {
            createdPopUp = null;
            if (!TryShowWindowBy(modal, out var window, overrideCanvasRoot))
            {
                return false;
            }

            createdPopUp = (IPopUp) window;
            return true;
        }

        /// <summary>
        /// Force show a pop-up. Typically used for critical pop-ups that must be shown immediately.
        /// The pop-up won't be enqueued, which needs manually handling the lifecycle.
        /// </summary>
        /// <param name="modal">The modal of the pop-up</param>
        public T ForceShow<T>(TModal modal) where T: TPopUp
        {
            var popUp = CreateWindowBy(modal);
            return (T) popUp;
        }
        
        protected override bool TryShowWindowBy(TModal modal, out TPopUp createdPopUp, RectTransform overrideCanvasRoot = null)
        {
            createdPopUp = default;
			
            // If there is a pop-up showing, enqueue the model.
            if (CurrentWindow != null)
            {
                AddToCollection(modal);
                return false;
            }

            var baseWindow = CreateWindowBy(modal, overrideCanvasRoot);
            CurrentWindow = baseWindow;
            return true;
        }

        protected override void AddToCollection(IQueueable windowOrModal)
        {
            WindowContainer.Enqueue(windowOrModal);
        }

        protected override bool TryRetrieveNextWindowOrModal(out IQueueable nextWindowOrModal)
        {
            nextWindowOrModal = null;
            
            if (WindowContainer.Count == 0)
            {
                return false;
            }
            
            nextWindowOrModal = WindowContainer.Dequeue();
            return true;
        }
    }
}