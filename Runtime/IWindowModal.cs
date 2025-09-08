namespace AYip.UI
{
	public interface IWindowModal
	{
		object PrefabKey { get; }
	}

	public interface IWindowModal<out TPrefabKey> : IWindowModal
	{
		new TPrefabKey PrefabKey { get; }
	}  
}