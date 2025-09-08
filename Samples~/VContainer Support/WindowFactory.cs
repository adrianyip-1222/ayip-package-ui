using AYip.Foundation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AYip.UI.VConatiner
{
	/// <summary>
	/// An example of how to use VContainer to instantiate windows with dependency injection support.
	/// </summary>
	public class WindowFactory<TPrefabKey> : DisposableBase, IWindowFactory
	{
		/// <summary>
		/// The prefab database to retrieve prefabs of the window by the prefab key.
		/// </summary>
		private readonly IPrefabDatabase<TPrefabKey> _prefabDatabase;
		private readonly IObjectResolver _objectResolver;
		
		public WindowFactory (
			IObjectResolver objectResolver,
			IPrefabDatabase<TPrefabKey> prefabDatabase
		) {
			_prefabDatabase = prefabDatabase;
			_objectResolver = objectResolver;
		}
		
		/// <summary>
		/// Create a window instance with the given modal and parent canvas root using VContainer for dependency injection.
		/// </summary>
		public IWindow Create<TModal>(TModal modal, RectTransform canvasRoot)
			where TModal : IWindowModal
		{
			var hasPrefab = _prefabDatabase.TryGetPrefab(modal.PrefabKey, out var prefab);
			if (!hasPrefab)
			{
				Debug.LogError($"Prefab with key {modal.PrefabKey} not found in the database.");
				return null;
			}

			var gameObjectScope = _objectResolver.CreateScope(builder =>
			{
				builder.RegisterInstance(modal).As(modal.GetType());
			});
			
			var instance = gameObjectScope.Instantiate(prefab, canvasRoot);
			return instance.GetComponentInChildren<IWindow>();
		}
	}
}
