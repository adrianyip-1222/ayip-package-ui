using AYip.UI.Pages;
using UnityEngine;

namespace AYip.UI.Samples.BasicExamples.Pages
{
	/// <summary>
	/// Create a wrapper class to define the page with given keys.
	/// </summary>
	public abstract class Page<TModal> : Page<PageType, TModal>, IWindowStateEventNotifier, IWindow
		where TModal : PageModal
	{
		public WindowStateEventListener EventListener { get; set; }

		protected override void PublishStateChangeEvent(WindowState changedState)
		{
			if (EventListener == null)
			{
				Debug.LogWarning ($"{name} doesn't have an EventListener assigned. State change event won't be published.");
				return;
			}
			
			EventListener.NotifyWindowStateChange(this, changedState);
		}

		public void InjectModal(IWindowModal modal)
		{
			LoadedModal = (TModal) modal;
			Initialize();
		}

		/// <summary>
		/// Initialize the page with the data in the modal.
		/// </summary>
		protected abstract void Initialize();
	}
}