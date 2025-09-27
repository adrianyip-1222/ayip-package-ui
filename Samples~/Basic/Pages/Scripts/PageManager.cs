using AYip.UI.Events;
using AYip.UI.Pages;
using UnityEngine;

namespace AYip.UI.Samples.BasicExamples.Pages
{
	/// <summary>
	/// Create a wrapper class to define the page manager with given keys.
	/// </summary>
	public class PageManager : PageManager<PageType, IPage<PageType, IPageModal<PageType>>, IPageModal<PageType>>
	{
		public PageManager(RectTransform defaultCanvasRoot, IWindowStateEventHandler windowStateEventHandler, IWindowFactory windowFactory) 
			: base(defaultCanvasRoot, windowStateEventHandler, windowFactory) { }
	}
}