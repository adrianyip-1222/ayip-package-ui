using System;
using UnityEngine;

namespace AYip.UI
{
	public interface IWindow : IEquatable<IWindow>
	{
		Guid Id { get; }
		GameObject GameObject { get; }
		IWindowModal LoadedModal { get; }
		bool Visibility { get; set; }
		void Close();
	}

	public interface IWindow<TPrefabKey, out TModal> : IWindow
		where TModal : IWindowModal<TPrefabKey>
	{
		new TModal LoadedModal { get; }
	}
}