using AYip.UI.PopUps;

namespace AYip.UI.UGUI
{
	/// <summary>
	/// The basic uGUI pop-up view class.
	/// </summary>
	public abstract class PopUp<TPrefabKey, TModel> : View<TPrefabKey, TModel>, IPopUp<TPrefabKey, TModel> where TModel : IPopUpModel<TPrefabKey> { }
}
