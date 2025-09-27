using AYip.Foundation;

namespace AYip.UI.Samples.BasicExamples.Pages
{
	public class PageFactory : WindowFactory<PageType>
	{
		private readonly WindowStateEventListener _windowStateEventListener;

		public PageFactory(IPrefabDatabase<PageType> prefabDatabase, WindowStateEventListener windowStateEventListener)
			: base(prefabDatabase)
		{
			_windowStateEventListener = windowStateEventListener;
		}
		
		protected override void PostInstantiate(IWindow instantiatedWindow)
		{
			if (instantiatedWindow is not IWindowStateEventNotifier notifier)
			{
				return;
			}

			notifier.EventListener = _windowStateEventListener;
		}
	}
}