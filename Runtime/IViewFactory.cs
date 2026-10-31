namespace AYip.UI
{
    /// <summary>
    /// Responsible for creating and tearing down views.
    /// </summary>
    /// <typeparam name="TRoot">The parent the views are spawned under, e.g. RectTransform for uGUI or VisualElement for UI Toolkit.</typeparam>
    public interface IViewFactory<in TRoot>
    {
        /// <summary>
        /// Try to spawn the view of the model under the root.
        /// </summary>
        IView TrySpawnView(IViewModel model, TRoot root);

        /// <summary>
        /// Tear down a view spawned by this factory, e.g. destroy its GameObject or remove its VisualElement from the hierarchy.
        /// </summary>
        /// <remarks>Called exactly once per view, by the view manager that spawned it.</remarks>
        void Despawn(IView view);
    }
}