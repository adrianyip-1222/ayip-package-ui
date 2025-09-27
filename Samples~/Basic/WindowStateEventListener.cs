using System.Collections.Generic;
using AYip.Foundation;
using AYip.UI.Events;
using Package = AYip.UI;

namespace AYip.UI.Samples.BasicExamples
{
	/// <summary>
	/// A simple implementation of IWindowStateEventHandler for demonstration purposes.
	/// You can use any event system you like, message pipes, event bus, or just use UnityEvents.
	/// </summary>
	public class WindowStateEventListener : DisposableBase, IWindowStateEventHandler
	{
		private readonly List<EventSubscriptionDTO> _eventSets = new(); 
		
		public void Subscribe(EventSubscriptionDTO eventSubscriptionDto)
		{
			_eventSets.Add(eventSubscriptionDto);
		}

		public void Unsubscribe(Package.IWindow window)
		{
			// Remove all event subscriptions related to the specified window
			_eventSets.RemoveAll(eventSet => eventSet.Window.Equals(window));
		}

		/// <summary>
		/// Invoke the state change event for the specified window and new state.
		/// </summary>
		/// <param name="window">The window whose state has changed.</param>
		/// <param name="newState">The new state of the window.</param>
		public void NotifyWindowStateChange(Package.IWindow window, WindowState newState)
		{
			var targetEvents = _eventSets.FindAll(eventSet => eventSet.Window.Equals(window) && eventSet.State == newState);
			
			foreach (var targetEvent in targetEvents)
			{
				targetEvent.OnStateChanged?.Invoke(window);
			}
		}
	}
}