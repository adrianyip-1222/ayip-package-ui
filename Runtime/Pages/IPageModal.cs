namespace AYip.UI.Pages
{
	/// <summary>
	/// The data modal for creating a page window.
	/// </summary>
	public interface IPageModal : IWindowModal, IStackable { }
	
	/// <summary>
	/// The data modal for creating a page window with a specific prefab key type.
	/// </summary>
	public interface IPageModal<out TPrefabKey> : IPageModal, IWindowModal<TPrefabKey> { }
}