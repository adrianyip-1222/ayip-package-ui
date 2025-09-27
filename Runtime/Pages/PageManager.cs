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
        where TPage : IPage<TPrefabKey, TModal>
        where TModal : IPageModal<TPrefabKey>
    {
        protected PageManager(
            RectTransform defaultCanvasRoot,
            IWindowStateEventHandler windowStateEventHandler,
            IWindowFactory windowFactory)
            : base(defaultCanvasRoot, windowStateEventHandler, windowFactory)
        {
            WindowContainer = new Stack<IStackable>();
        }

        /// <summary>
        /// The count of the pages or modals in the stack at the back.
        /// </summary>
        public override int StackCounts => WindowContainer.Count;

        /// <summary>
        /// Show the page immediately and stack up the current page if any. (Show and forget pattern)
        /// </summary>
        /// <param name="modal">The modal of the page</param>
        public void Show(TModal modal, RectTransform overrideCanvasRoot = null)
        {
            Show(modal, out _, overrideCanvasRoot);
        }
		
        /// <summary>
        /// Show the page immediately and stack up the current page if any.
        /// </summary>
        /// <param name="modal">The modal of the page</param>
        /// <param name="showedPage">The page to show.</param>
        public void Show(TModal modal, out IPage showedPage, RectTransform overrideCanvasRoot = null)
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
            
            if (!TryShowWindowBy(modal, out var window, overrideCanvasRoot))
            {
                return;
            }

            CurrentWindow = window;
            showedPage = window;
        }

        protected override bool TryShowWindowBy(TModal modal, out TPage createPage, RectTransform overrideCanvasRoot = null)
        {
            var window = CreateWindowBy(modal, overrideCanvasRoot);
            createPage = window;
            return true;
        }

        protected override void AddToCollection(IStackable windowOrModal)
        {
            WindowContainer.Push(windowOrModal);
        }

        protected override bool TryRetrieveNextWindowOrModal(out IStackable nextWindowOrModal)
        {
            nextWindowOrModal = null;
            
            if (WindowContainer.Count == 0)
            {
                return false;
            }
            
            nextWindowOrModal = WindowContainer.Pop();
            return true;
        }
    }
}