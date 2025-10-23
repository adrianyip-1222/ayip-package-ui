using System;
using UnityEngine;

namespace AYip.UI
{
	public interface IView : IEquatable<IView>
	{
		Guid Id { get; }
		GameObject GameObject { get; }
		IViewModel LoadedModel { get; }
		bool Visibility { get; set; }
		void Close();
	}

	public interface IView<TPrefabKey, out TModel> : IView
		where TModel : IViewModel<TPrefabKey>
	{
		new TModel LoadedModel { get; }
	}
}