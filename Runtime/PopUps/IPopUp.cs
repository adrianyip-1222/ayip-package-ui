namespace AYip.UI.PopUps
{
	/// <summary>
	/// The base interface for all pop-ups.
	/// </summary>
	public interface IPopUp : IWindow, IQueueable { }

	/// <summary>
	/// A generic interface for pop-ups with specified prefab key, pop-up type, and modal type.
	/// </summary>
	public interface IPopUp<TPrefabKey, TPopUp, out TModal> : IPopUp, IWindow<TPrefabKey, TPopUp, TModal>
		where TModal : IPopUpModal<TPrefabKey>
		where TPopUp : IPopUp<TPrefabKey, TPopUp, TModal> { }
}