# Changelog

## [2.0.0] - 2026-10-31

### Added
- UI Toolkit support: `AYip.UI.Toolkit.View`, `View<TPrefabKey, TModel>`, `Page<TPrefabKey, TModel>` and `PopUp<TPrefabKey, TModel>`, plain classes owning a `VisualElement` root.
- `IViewFactory<TRoot>.Despawn(IView)`: the factory owns teardown.

### Changed (breaking)
- The view root is generic. `ViewManager`, `PageManager`, `PopUpManager` and `IViewFactory` take a `TRoot` type parameter (first) instead of the hard-coded `RectTransform`.
- `IView.GameObject` is removed. The core has no `GameObject`.
- The uGUI bases moved from `AYip.UI.View`, `AYip.UI.Pages.Page` and `AYip.UI.PopUps.PopUp` to `AYip.UI.UGUI.View`, `AYip.UI.UGUI.Page` and `AYip.UI.UGUI.PopUp`.
- `View.Close()` only publishes `ViewState.Closed`. It no longer destroys the GameObject; the managing view manager despawns the view through its factory.
- `Visibility` now drives the view: uGUI toggles `gameObject.SetActive`, UI Toolkit toggles `style.display`.
- Renamed: `DefaultCanvasRoot` to `DefaultRoot`, the `overrideCanvasRoot` parameters to `overrideRoot`.
- Requires Unity 2021.2 (runtime UI Toolkit).

### Fixed
- Closing a page restored a protected page without listening to its close: the closed view was re-subscribed instead of the restored one. A protected page now stops listening while it waits in the stack and listens again once it is current.
- A view was destroyed twice on close (by `View.Close()` and by the manager). Teardown now happens exactly once, in the factory.
- Closing a force-shown pop-up cleared the current pop-up and dequeued the next one. It now only despawns itself.
- `PopUpManager` never returned the pop-up it showed: `TryShowOrEnqueue(model, out createdPopUp)` gave null, and closing a pop-up left the next queued one shown but not current.
- An unprotected page replaced by `PageManager.Show` stayed subscribed to its close event after it was destroyed.

### Migration from 1.x
1. Add the root type to every manager: `PageManager<TPrefabKey, TPage, TModel>` becomes `PageManager<RectTransform, TPrefabKey, TPage, TModel>`; the same for `PopUpManager`.
2. `IViewFactory` becomes `IViewFactory<RectTransform>`, and implement `Despawn(IView view)`, e.g. `Object.Destroy(((Component)view).gameObject)`.
3. Change `using AYip.UI.Pages;` / `using AYip.UI.PopUps;` base classes to `AYip.UI.UGUI.Page` / `AYip.UI.UGUI.PopUp`; `AYip.UI.View` to `AYip.UI.UGUI.View`.
4. Replace reads of `IView.GameObject` with a cast to `Component`.
5. Code that relied on `Close()` destroying an unmanaged view must now despawn it itself.
