namespace AYip.UI.Pages
{
	/// <summary>
	/// The base interface for all pages.
	/// </summary>
	public interface IPage : IView, IStackable
	{
		/// <summary>
		/// Indicates whether the page is protected from being destroyed when a new page is shown.
		/// </summary>
		bool IsProtected { get; }
	}

	/// <summary>
	/// A generic interface for pages with specified prefab key, page type, and model type.
	/// </summary>
	public interface IPage<TPrefabKey, out TModel> : IPage, IView<TPrefabKey, TModel>
		where TModel : IPageModel<TPrefabKey> { }
}