using AYip.UI.Pages;

namespace AYip.UI.VConatiner.Pages
{
    /// <summary>
    /// An example of how to wrap the base page class to use VContainer to inject pop-up modal
    /// </summary>
    public abstract class Page<TPrefabKey, TPage, TModal> : Window<TPrefabKey, TPage, TModal>, IPage<TPrefabKey, TPage, TModal>
        where TPage : IPage<TPrefabKey, TPage, TModal>
        where TModal : IPageModal<TPrefabKey>
    {
        public abstract bool IsProtected { get; }
    }
}