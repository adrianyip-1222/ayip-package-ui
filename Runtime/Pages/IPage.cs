namespace AYip.UI.Pages
{
	/// <summary>
	/// The base interface for all pages.
	/// </summary>
	public interface IPage : IWindow, IStackable
	{
		/// <summary>
		/// Indicates whether the page is protected from being destroyed when a new page is shown.
		/// </summary>
		bool IsProtected { get; }
	}

	/// <summary>
	/// A generic interface for pages with specified prefab key, page type, and modal type.
	/// </summary>
	public interface IPage<TPrefabKey, TPage, out TModal> : IPage, IWindow<TPrefabKey, TPage, TModal>
		where TModal : IPageModal<TPrefabKey>
		where TPage : IPage<TPrefabKey, TPage, TModal> { }
}