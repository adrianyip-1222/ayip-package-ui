using AYip.UI.Pages;
using UnityEngine.UIElements;

namespace AYip.UI.Toolkit
{
	/// <summary>
	/// The basic UI Toolkit page view class.
	/// </summary>
	public abstract class Page<TPrefabKey, TModel> : View<TPrefabKey, TModel>, IPage<TPrefabKey, TModel>
		where TModel : IPageModel<TPrefabKey>
	{
		protected Page(VisualElement root, TModel model) : base(root, model) { }

		public abstract bool IsProtected { get; protected set; }
	}
}
