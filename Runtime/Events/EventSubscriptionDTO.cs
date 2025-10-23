using UnityEngine.Events;

namespace AYip.UI.Events
{
    /// <summary>
    /// A data transfer object to hold the subscription information.
    /// </summary>
    public readonly struct EventSubscriptionDTO
    {
        public EventSubscriptionDTO(IView view, ViewState state, UnityAction<IView, ViewState> onStateChanged, int priority = 0)
        {
            View = view;
            State = state;
            OnStateChanged = onStateChanged;
            Priority = priority;
        }

        /// <summary>
        /// The view to subscribe to.
        /// </summary>
        public IView View { get; }
        
        /// <summary>
        /// The state to listen for.
        /// </summary>
        public ViewState State { get; }
        
        /// <summary>
        /// The callback to invoke when the state changes.
        /// </summary>
        public UnityAction<IView, ViewState> OnStateChanged { get; }

        /// <summary>
        /// The priority of the event subscription. Higher priority events are invoked first.
        /// </summary>
        public int Priority { get; }
    }
}