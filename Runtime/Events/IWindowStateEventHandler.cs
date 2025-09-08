namespace AYip.UI.Events
{
	/// <summary>
	/// Responsible for handling state events of the window. It empowers the flexibility of using different event systems for your project.
	/// </summary>
	public interface IWindowStateEventHandler
	{
		/// <summary>
		/// Subscribe to window state change events.
		/// </summary>
		/// <param name="eventSubscriptionDto"></param>
		void Subscribe(EventSubscriptionDTO eventSubscriptionDto);
		
		/// <summary>
		/// Unsubscribe from window state change events.
		/// </summary>
		void Unsubscribe(IWindow window);
	}
}