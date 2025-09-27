namespace AYip.UI.Samples.BasicExamples
{
	/// <summary>
	/// Indicates that a window has a ability of notifying the window state change directly.
	/// </summary>
	public interface IWindowStateEventNotifier
	{
		WindowStateEventListener EventListener { get; set; }
	}
}