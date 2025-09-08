using System;
using UnityEngine.Events;

namespace AYip.UI.Events
{
    /// <summary>
    /// A data transfer object to hold the subscription information.
    /// </summary>
    public readonly struct EventSubscriptionDTO
    {
        public EventSubscriptionDTO(IWindow window, WindowState state, UnityAction<IWindow> onStateChanged)
        {
            Window = window;
            State = state;
            OnStateChanged = onStateChanged;
        }

        /// <summary>
        /// The window to subscribe to.
        /// </summary>
        public IWindow Window { get; }
        
        /// <summary>
        /// The state to listen for.
        /// </summary>
        public WindowState State { get; }
        
        /// <summary>
        /// The callback to invoke when the state changes.
        /// </summary>
        public UnityAction<IWindow> OnStateChanged { get; }

        public bool Equals(IWindow other)
        {
            return false;
        }
    }
}