namespace AYip.UI.PopUps
{
	/// <summary>
	/// The data model for creating a pop-up view.
	/// </summary>
	public interface IPopUpModel : IViewModel, IQueueable { }
	
	/// <summary>
	/// The data model for creating a pop-up view with a specific prefab key type.
	/// </summary>
	public interface IPopUpModel<out TPrefabKey> : IPopUpModel, IViewModel<TPrefabKey> { }
}