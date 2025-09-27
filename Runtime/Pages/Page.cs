namespace AYip.UI.Pages
{
	/// <summary>
	/// The basic page window class.
	/// </summary>
	public abstract class Page<TPrefabKey, TModal> : Window<TPrefabKey, TModal>, IPage<TPrefabKey, TModal>
		where TModal : IPageModal<TPrefabKey>
	{
		public abstract bool IsProtected { get; protected set; }
	}
}