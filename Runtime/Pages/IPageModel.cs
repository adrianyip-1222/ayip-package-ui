namespace AYip.UI.Pages
{
	/// <summary>
	/// The data model for creating a page view.
	/// </summary>
	public interface IPageModel : IViewModel, IStackable { }
	
	/// <summary>
	/// The data model for creating a page view with a specific prefab key type.
	/// </summary>
	public interface IPageModel<out TPrefabKey> : IPageModel, IViewModel<TPrefabKey> { }
}