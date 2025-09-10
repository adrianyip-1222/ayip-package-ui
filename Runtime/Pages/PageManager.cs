using System.Collections.Generic;
using AYip.UI.Events;
using UnityEngine;

namespace AYip.UI.Pages
{
    /// <summary>
    /// A manager for pages that handles showing and stacking them based on their modals.
    /// This manager allows for showing pages immediately if no other page is currently active,
    /// or stacking them for later display if a page is already showing.
    /// </summary>
    public abstract class PageManager<TPrefabKey, TPage, TModal> : WindowManager<TPrefabKey, TPage, Stack<IStackable>, IStackable, TModal>
        where TPage : IPage<TPrefabKey, TPage, TModal>
        where TModal : IPageModal<TPrefabKey>
    {
        protected PageManager(
            RectTransform canvasRoot,
            IWindowStateEventHandler windowStateEventHandler,
            IWindowFactory windowFactory)
            : base(canvasRoot, windowStateEventHandler, windowFactory)
        {
            WindowCollection = new Stack<IStackable>();
        }
		
        /// <summary>
        /// Show the page immediately and stack up the current page if any.
        /// </summary>
        /// <param name="modal">The modal of the page</param>
        /// <param name="showedPage">The page to show.</param>
        public void Show(TModal modal, out IPage showedPage)
        {
            showedPage = null; 
            
            // If there is a current page shown, stack it up.
            if (CurrentWindow != null)
            {
                // Handle how to stack up the current page.
                if (CurrentWindow.IsProtected)
                {
                    AddToCollection(CurrentWindow);
                }
                else
                {
                    AddToCollection(CurrentWindow.LoadedModal);
                    
                    // Destroy the current view.
                    Object.Destroy(CurrentWindow.GameObject);
                }
            }
            
            if (!TryShowWindowBy(modal, out var window))
            {
                return;
            }

            CurrentWindow = window;
            showedPage = (IPage) window;
        }

        protected override bool TryShowWindowBy(TModal modal, out TPage createPage)
        {
            var window = CreateWindowBy(modal);
            createPage = window;
            return true;
        }

        protected override void AddToCollection(IStackable windowOrModal)
        {
            WindowCollection.Push(windowOrModal);
        }

        protected override bool TryRetrieveNextWindowOrModal(out IStackable nextWindowOrModal)
        {
            nextWindowOrModal = null;
            
            if (WindowCollection.Count == 0)
            {
                return false;
            }
            
            nextWindowOrModal = WindowCollection.Pop();
            return true;
        }
    }
}