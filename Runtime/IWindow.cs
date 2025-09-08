using System;

namespace AYip.UI
{
	public interface IWindow : IEquatable<IWindow>
	{
		Guid Id { get; }
		IWindowModal LoadedModal { get; }
		bool Visibility { get; set; }
		void Close();
	}

	public interface IWindow<TPrefabKey, TWindow, out TModal> : IWindow
		where TModal : IWindowModal<TPrefabKey>
		where TWindow : IWindow<TPrefabKey, TWindow, TModal>
	{
		new TModal LoadedModal { get; }
	}
}