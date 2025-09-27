using AYip.Foundation;

namespace AYip.UI.Samples.BasicExamples.PopUps
{
	public class PopUpFactory : WindowFactory<PopUpType>
	{
		private readonly WindowStateEventListener _windowStateEventListener;

		public PopUpFactory(IPrefabDatabase<PopUpType> prefabDatabase, WindowStateEventListener windowStateEventListener)
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