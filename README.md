# AYip UI Management

Manages pages (a stack) and pop-ups (a FIFO queue) for both uGUI and UI Toolkit. Requires Unity 2021.2 or later.

- `PageManager<TRoot, TPrefabKey, TPage, TModel>`: `Show` puts a page on top. The page below is kept alive if it is protected, otherwise it is despawned and its model is stacked. Closing the top page brings back the one below.
- `PopUpManager<TRoot, TPrefabKey, TPopUp, TModel>`: `TryShowOrEnqueue` shows a pop-up or queues it behind the current one. `ForceShow` bypasses the queue.
- `IViewFactory<TRoot>` spawns views under a root and despawns them. The managers never destroy anything themselves; they call `Despawn` exactly once per view.
- `TRoot` is `RectTransform` for uGUI and `VisualElement` for UI Toolkit.

## uGUI

Views are MonoBehaviours on prefabs: derive from `AYip.UI.UGUI.Page<TPrefabKey, TModel>` or `AYip.UI.UGUI.PopUp<TPrefabKey, TModel>`.

```csharp
public class UGUIFactory : IViewFactory<RectTransform>
{
	public IView TrySpawnView(IViewModel model, RectTransform root)
	{
		var prefab = _prefabs.Get(model.PrefabKey);	// your lookup
		return Object.Instantiate(prefab, root).GetComponent<IView>();
	}

	public void Despawn(IView view) => Object.Destroy(((Component)view).gameObject);
}

public class MenuPageManager : PageManager<RectTransform, string, MenuPage, MenuModel>
{
	public MenuPageManager(RectTransform root, IHandler handler, IViewFactory<RectTransform> factory)
		: base(root, handler, factory) { }
}
```

## UI Toolkit

Views are plain classes owning a `VisualElement` root: derive from `AYip.UI.Toolkit.Page<TPrefabKey, TModel>` or `AYip.UI.Toolkit.PopUp<TPrefabKey, TModel>`.

```csharp
public class ConfirmPopUp : AYip.UI.Toolkit.PopUp<string, ConfirmModel>
{
	private readonly ViewStateEventHandler _handler;

	public ConfirmPopUp(VisualElement root, ConfirmModel model, ViewStateEventHandler handler) : base(root, model)
	{
		_handler = handler;
		root.Q<Button>("ok").clicked += Close;
	}

	protected override void PublishStateChangeEvent(ViewState state) => _handler.NotifyViewStateChange(this, state);
}

public class ToolkitFactory : IViewFactory<VisualElement>
{
	public IView TrySpawnView(IViewModel model, VisualElement root)
	{
		var element = _trees.Get(model.PrefabKey).Instantiate();	// your VisualTreeAsset lookup
		root.Add(element);
		return new ConfirmPopUp(element, (ConfirmModel)model, _handler);
	}

	public void Despawn(IView view) => ((AYip.UI.Toolkit.View)view).Root.RemoveFromHierarchy();
}

public class ConfirmPopUpManager : PopUpManager<VisualElement, string, ConfirmPopUp, ConfirmModel>
{
	public ConfirmPopUpManager(VisualElement root, IHandler handler, IViewFactory<VisualElement> factory)
		: base(root, handler, factory) { }
}
```

The default root is usually `UIDocument.rootVisualElement`.
