namespace AYip.UI.PopUps
{
	/// <summary>
	/// The basic pop-up window class.
	/// </summary>
	public abstract class PopUp<TPrefabKey, TPopUp, TModal> : Window<TPrefabKey, TPopUp, TModal>, IPopUp<TPrefabKey, TPopUp, TModal>
		where TPopUp : IPopUp<TPrefabKey, TPopUp, TModal>
		where TModal : IPopUpModal<TPrefabKey> { }
}