using AYip.UI.PopUps;
using UnityEngine.UIElements;

namespace AYip.UI.Toolkit
{
	/// <summary>
	/// The basic UI Toolkit pop-up view class.
	/// </summary>
	public abstract class PopUp<TPrefabKey, TModel> : View<TPrefabKey, TModel>, IPopUp<TPrefabKey, TModel>
		where TModel : IPopUpModel<TPrefabKey>
	{
		protected PopUp(VisualElement root, TModel model) : base(root, model) { }
	}
}
