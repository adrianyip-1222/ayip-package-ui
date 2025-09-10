namespace AYip.UI.Pages
{
	/// <summary>
	/// The basic page window class.
	/// </summary>
	public abstract class Page<TPrefabKey, TPage, TModal> : Window<TPrefabKey, TPage, TModal>, IPage<TPrefabKey, TPage, TModal>
		where TPage : IPage<TPrefabKey, TPage, TModal>
		where TModal : IPageModal<TPrefabKey>
	{
		public abstract bool IsProtected { get; }
	}
}