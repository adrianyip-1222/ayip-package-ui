using System;

namespace AYip.UI
{
	/// <summary>
	/// A view managed by a view manager. It is UI framework agnostic: the factory that spawned it owns its teardown.
	/// </summary>
	public interface IView : IEquatable<IView>
	{
		Guid Id { get; }
		IViewModel LoadedModel { get; }
		bool Visibility { get; set; }

		/// <summary>
		/// Publish the closed state. The managing view manager then despawns the view through its factory.
		/// </summary>
		void Close();
	}

	public interface IView<TPrefabKey, out TModel> : IView
		where TModel : IViewModel<TPrefabKey>
	{
		new TModel LoadedModel { get; }
	}
}