using AYip.Foundation;
using AYip.UI.Pages;

namespace AYip.UI.Samples.BasicExamples.Pages
{
	public abstract class PageModal : DisposableBase, IPageModal<PageType>
	{
		/// <summary>
		/// The prefab key for instantiating the pop-up.
		/// </summary>
		public abstract PageType PrefabKey { get; }
		object IWindowModal.PrefabKey => PrefabKey;
	}
}