using AYip.Foundation;
using UnityEngine;
using Package = AYip.UI;

namespace AYip.UI.Samples.BasicExamples
{
	/// <summary>
	/// Create a class for instantiating windows (both pop-ups and pages).
	/// </summary>
	public abstract class WindowFactory<TPrefabKey> : DisposableBase, IWindowFactory
	{
		private readonly IPrefabDatabase<TPrefabKey> _prefabDatabase;

		protected WindowFactory(IPrefabDatabase<TPrefabKey> prefabDatabase)
		{
			_prefabDatabase = prefabDatabase;
		}

		public Package.IWindow Create<TModal>(TModal modal, RectTransform canvasRoot) where TModal : IWindowModal
		{
			if (!_prefabDatabase.TryGetPrefab(modal.PrefabKey, out var prefab))
			{
				Debug.LogError($"No prefab found for modal of type {modal.GetType().Name}.");
				return null;
			}

			var instance = Object.Instantiate(prefab, canvasRoot);
			if (!instance.TryGetComponent<IWindow>(out var window))
			{
				Debug.LogError($"Instantiated prefab does not contain a component that implements IWindow.");
				Object.Destroy(instance);
				return null;
			}
			
			// Inject the modal into the window.
			// If you use VContainer by importing the VContainer support from package sample page.
			// The initialization can be done by the windows' Awake() or Start() since the LoadedModal will be injected.
			window.InjectModal(modal);

			PostInstantiate(window);
			
			return window;
		}

		protected abstract void PostInstantiate(IWindow instantiatedWindow);
	}
}