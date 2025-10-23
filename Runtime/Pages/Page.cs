namespace AYip.UI.Pages
{
	/// <summary>
	/// The basic page view class.
	/// </summary>
	public abstract class Page<TPrefabKey, TModel> : View<TPrefabKey, TModel>, IPage<TPrefabKey, TModel>
		where TModel : IPageModel<TPrefabKey>
	{
		public abstract bool IsProtected { get; protected set; }
	}
}