using UnityEngine.Events;

namespace AYip.UI.Events
{
	/// <summary>
	/// Responsible for handling state events of the view. It empowers the flexibility of using different event systems for your project.
	/// </summary>
	public interface IHandler
	{
		/// <summary>
		/// Subscribe to view state change events.
		/// </summary>
		void Subscribe(IView targetView, ViewState targetState, UnityAction<IView, ViewState> onStateChanged);
		
		/// <summary>
		/// Unsubscribe from view state change events.
		/// </summary>
		void Unsubscribe(IView view);
	}
}