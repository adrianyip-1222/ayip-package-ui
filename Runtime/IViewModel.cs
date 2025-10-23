namespace AYip.UI
{
	public interface IViewModel
	{
		object PrefabKey { get; }
	}

	public interface IViewModel<out TPrefabKey> : IViewModel
	{
		new TPrefabKey PrefabKey { get; }
	}  
}