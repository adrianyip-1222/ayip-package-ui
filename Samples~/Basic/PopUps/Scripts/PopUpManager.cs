using AYip.UI.Events;
using AYip.UI.PopUps;
using UnityEngine;

namespace AYip.UI.Samples.BasicExamples.PopUps
{
	/// <summary>
	/// Create a wrapper class to define the PopUpManager with given keys.
	/// </summary>
	public class PopUpManager : PopUpManager<PopUpType, PopUp<PopUpModal>, PopUpModal>
	{
		public PopUpManager(RectTransform defaultCanvasRoot, IWindowStateEventHandler windowStateEventHandler, IWindowFactory windowFactory) 
			: base(defaultCanvasRoot, windowStateEventHandler, windowFactory) { }
	}
}