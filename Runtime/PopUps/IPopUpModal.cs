namespace AYip.UI.PopUps
{
	/// <summary>
	/// The data modal for creating a pop-up window.
	/// </summary>
	public interface IPopUpModal : IWindowModal, IQueueable { }
	
	/// <summary>
	/// The data modal for creating a pop-up window with a specific prefab key type.
	/// </summary>
	public interface IPopUpModal<out TPrefabKey> : IPopUpModal, IWindowModal<TPrefabKey> { }
}