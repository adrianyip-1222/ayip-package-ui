using AYip.UI.Pages;

namespace AYip.UI.UGUI
{
	/// <summary>
	/// The basic uGUI page view class.
	/// </summary>
	public abstract class Page<TPrefabKey, TModel> : View<TPrefabKey, TModel>, IPage<TPrefabKey, TModel>
		where TModel : IPageModel<TPrefabKey>
	{
		public abstract bool IsProtected { get; protected set; }
	}
}
