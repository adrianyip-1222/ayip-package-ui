using System;
using UnityEngine;

namespace AYip.UI
{
	public abstract class Window : MonoBehaviour, IWindow
	{
		public abstract Guid Id { get; }
		public IWindowModal LoadedModal { get; protected set; }
		public bool Visibility { get; set; }
		public abstract void Close();
		
		public bool Equals(IWindow other)
		{
			return other is not null && other.Id.Equals(Id);
		}
	}
	
	public abstract class Window<TPrefabKey, TWindow, TModal> : Window, IWindow<TPrefabKey, TWindow, TModal>
		where TWindow : IWindow<TPrefabKey, TWindow, TModal>
		where TModal : IWindowModal<TPrefabKey>
	{
		public override Guid Id { get; } = Guid.NewGuid();

		public new TModal LoadedModal
		{
			get => (TModal)base.LoadedModal;
			protected set => base.LoadedModal = value;
		}
		IWindowModal IWindow.LoadedModal => LoadedModal;

		public override void Close()
		{
			PublishStateChangeEvent(WindowState.Closed);
			Destroy(gameObject);
		}
	
		/// <summary>
		/// Publish the event of the window state change.
		/// </summary>
		protected abstract void PublishStateChangeEvent(WindowState changedState);
	}
}