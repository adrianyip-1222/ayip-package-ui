namespace AYip.UI.PopUps
{
	/// <summary>
	/// The basic pop-up view class.
	/// </summary>
	public abstract class PopUp<TPrefabKey, TModel> : View<TPrefabKey, TModel>, IPopUp<TPrefabKey, TModel> where TModel : IPopUpModel<TPrefabKey> { }
}