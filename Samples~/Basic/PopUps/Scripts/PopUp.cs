using AYip.UI.PopUps;
using UnityEngine;

namespace AYip.UI.Samples.BasicExamples.PopUps
{
	/// <summary>
	/// Create a wrapper class to define the PopUp with given keys.
	/// </summary>
	public abstract class PopUp<TModal> : PopUp<PopUpType, TModal>, IWindowStateEventNotifier
		where TModal : PopUpModal
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
	}
}