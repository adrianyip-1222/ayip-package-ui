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
        where TPopUp : IPopUp<TPrefabKey, TPopUp, TModal>
        where TModal : IPopUpModal<TPrefabKey>
    {
        protected PopUpManager(
            RectTransform canvasRoot,
            IWindowStateEventHandler windowStateEventHandler,
            IWindowFactory windowFactory)
            : base(canvasRoot, windowStateEventHandler, windowFactory)
        {
            WindowCollection = new Queue<IQueueable>();
        }
		
        /// <summary>
        /// Attempts to show the pop-up by its modal immediately or enqueue it if there is a pop-up already showing.
        /// </summary>
        /// <param name="modal">The modal of the pop-up</param>
        /// <param name="createdPopUp">The created pop-up if it can be shown immediately, otherwise default.</param>
        /// <returns>If the pop-up was shown immediately.</returns>
        public bool TryShowOrEnqueue(TModal modal, out IPopUp createdPopUp)
        {
            createdPopUp = null;
            if (!TryShowWindowBy(modal, out var window))
            {
                return false;
            }

            createdPopUp = (IPopUp) window;
            return true;
        }

        protected override bool TryShowWindowBy(TModal modal, out TPopUp createdPopUp)
        {
            createdPopUp = default;
			
            // If there is a pop-up showing, enqueue the model.
            if (CurrentWindow != null)
            {
                AddToCollection(modal);
                return false;
            }

            var baseWindow = CreateWindowBy(modal);
            CurrentWindow = baseWindow;
            return true;
        }

        protected override void AddToCollection(IQueueable windowOrModal)
        {
            WindowCollection.Enqueue(windowOrModal);
        }

        protected override bool TryRetrieveNextWindowOrModal(out IQueueable nextWindowOrModal)
        {
            nextWindowOrModal = null;
            
            if (WindowCollection.Count == 0)
            {
                return false;
            }
            
            nextWindowOrModal = WindowCollection.Dequeue();
            return true;
        }
    }
}