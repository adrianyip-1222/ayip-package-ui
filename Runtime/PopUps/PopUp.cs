namespace AYip.UI.PopUps
{
	/// <summary>
	/// The basic pop-up window class.
	/// </summary>
	public abstract class PopUp<TPrefabKey, TModal> : Window<TPrefabKey, TModal>, IPopUp<TPrefabKey, TModal> where TModal : IPopUpModal<TPrefabKey> { }
}