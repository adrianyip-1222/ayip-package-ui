namespace AYip.UI.PopUps
{
	/// <summary>
	/// The base interface for all pop-ups.
	/// </summary>
	public interface IPopUp : IView, IQueueable { }

	/// <summary>
	/// A generic interface for pop-ups with specified prefab key, pop-up type, and model type.
	/// </summary>
	public interface IPopUp<TPrefabKey, out TModel> : IPopUp, IView<TPrefabKey, TModel>
		where TModel : IPopUpModel<TPrefabKey> { }
}