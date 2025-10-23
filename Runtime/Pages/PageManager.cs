using System.Collections.Generic;
using AYip.UI.Events;
using UnityEngine;

namespace AYip.UI.Pages
{
    /// <summary>
    /// A manager for pages that handles showing and stacking them based on their models.
    /// This manager allows for showing pages immediately if no other page is currently active,
    /// or stacking them for later display if a page is already showing.
    /// </summary>
    public abstract class PageManager<TPrefabKey, TPage, TModel> : ViewManager<TPrefabKey, TPage, Stack<IStackable>, IStackable, TModel>
        where TPage : IPage<TPrefabKey, TModel>
        where TModel : IPageModel<TPrefabKey>
    {
        protected PageManager(
            RectTransform defaultCanvasRoot,
            IHandler viewStateEventHandler,
            IViewFactory viewFactory)
            : base(defaultCanvasRoot, viewStateEventHandler, viewFactory)
        {
            ViewContainer = new Stack<IStackable>();
        }

        /// <summary>
        /// The count of the pages or models in the stack at the back.
        /// </summary>
        public override int StackCounts => ViewContainer.Count;

        /// <summary>
        /// Show the page immediately and stack up the current page if any. (Show and forget pattern)
        /// </summary>
        /// <param name="model">The model of the page</param>
        public void Show(TModel model, RectTransform overrideCanvasRoot = null)
        {
            Show(model, out _, overrideCanvasRoot);
        }
		
        /// <summary>
        /// Show the page immediately and stack up the current page if any.
        /// </summary>
        /// <param name="model">The model of the page</param>
        /// <param name="showedPage">The page to show.</param>
        public void Show(TModel model, out IPage showedPage, RectTransform overrideCanvasRoot = null)
        {
            showedPage = null; 
            
            // If there is a current page shown, stack it up.
            if (CurrentView != null)
            {
                // Handle how to stack up the current page.
                if (CurrentView.IsProtected)
                {
                    AddToCollection(CurrentView);
                }
                else
                {
                    AddToCollection(CurrentView.LoadedModel);
                    
                    // Destroy the current view.
                    Object.Destroy(CurrentView.GameObject);
                }
            }
            
            if (!TryShowViewBy(model, out var view, overrideCanvasRoot))
            {
                return;
            }

            CurrentView = view;
            showedPage = view;
        }

        protected override bool TryShowViewBy(TModel model, out TPage createPage, RectTransform overrideCanvasRoot = null)
        {
            var view = CreateViewBy(model, overrideCanvasRoot);
            createPage = view;
            return true;
        }

        protected override void AddToCollection(IStackable viewOrModel)
        {
            ViewContainer.Push(viewOrModel);
        }

        protected override bool TryRetrieveNextViewOrModel(out IStackable nextViewOrModel)
        {
            nextViewOrModel = null;
            
            if (ViewContainer.Count == 0)
            {
                return false;
            }
            
            nextViewOrModel = ViewContainer.Pop();
            return true;
        }
    }
}