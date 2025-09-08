using AYip.UI.PopUps;

namespace AYip.UI.VConatiner.PopUps
{
	/// <summary>
	/// An example of how to wrap the base PopUp class to use VContainer to inject pop-up modal
	/// </summary>
	public abstract class PopUp<TPrefabKey, TPopUp, TModal> : Window<TPrefabKey, TPopUp, TModal>, IPopUp<TPrefabKey, TPopUp, TModal>
		where TPopUp : IPopUp<TPrefabKey, TPopUp, TModal>
		where TModal : IPopUpModal<TPrefabKey> { }
}