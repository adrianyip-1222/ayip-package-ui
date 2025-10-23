using UnityEngine;

namespace AYip.UI
{
    /// <summary>
    /// Responsible for creating views.
    /// </summary>
    public interface IViewFactory
    {
        /// <summary>
        /// Try to instantiate a view of type TModel.
        /// </summary>
        IView TrySpawnView(IViewModel model, RectTransform canvasRoot);
    }
}