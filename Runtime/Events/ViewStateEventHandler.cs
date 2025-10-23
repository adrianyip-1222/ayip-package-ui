using System.Collections.Generic;
using AYip.Foundation;
using UnityEngine.Events;

namespace AYip.UI.Events
{
    /// <summary>
    /// A simple implementation of IViewStateEventHandler for demonstration purposes.
    /// You can use any event system you like, message pipes, event bus, or just use UnityEvents.
    /// </summary>
    public class ViewStateEventHandler : DisposableBase, IHandler
    {
        private readonly List<EventSubscriptionDTO> _subscriptions = new(); 
		
        public void Subscribe(IView targetView, ViewState targetState, UnityAction<IView, ViewState> onStateChanged)
        {
            var eventSubscriptionDto = new EventSubscriptionDTO(
			
                targetView,
                targetState,
                onStateChanged
            );
            _subscriptions.Add(eventSubscriptionDto);
        }
		
        public void Unsubscribe(IView view)
        {
            // Remove all event subscriptions related to the specified view
            _subscriptions.RemoveAll(eventSet => eventSet.View.Equals(view));
        }

        /// <summary>
        /// Invoke the state change event for the specified view and new state.
        /// </summary>
        /// <param name="view">The view whose state has changed.</param>
        /// <param name="newState">The new state of the view.</param>
        public void NotifyViewStateChange(IView view, ViewState newState)
        {
            var targetEvents = _subscriptions.FindAll(eventSet => eventSet.View.Equals(view) && eventSet.State == newState);
			
            foreach (var targetEvent in targetEvents)
            {
                targetEvent.OnStateChanged?.Invoke(view, newState);
            }
        }
    }
}