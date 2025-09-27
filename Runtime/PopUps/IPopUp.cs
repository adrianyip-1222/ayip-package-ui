namespace AYip.UI.PopUps
{
	/// <summary>
	/// The base interface for all pop-ups.
	/// </summary>
	public interface IPopUp : IWindow, IQueueable { }

	/// <summary>
	/// A generic interface for pop-ups with specified prefab key, pop-up type, and modal type.
	/// </summary>
	public interface IPopUp<TPrefabKey, out TModal> : IPopUp, IWindow<TPrefabKey, TModal>
		where TModal : IPopUpModal<TPrefabKey> { }
}