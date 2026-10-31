using System;
using UnityEngine.UIElements;

namespace AYip.UI.Toolkit
{
	/// <summary>
	/// The base of a UI Toolkit view: a plain class owning a VisualElement root, e.g. cloned from a VisualTreeAsset by the factory.
	/// </summary>
	public abstract class View : IView
	{
		protected View(VisualElement root)
		{
			Root = root;
		}

		/// <summary>
		/// The root element of the view. The factory adds it under the manager's root and removes it on despawn.
		/// </summary>
		public VisualElement Root { get; }

		public abstract Guid Id { get; }
		public IViewModel LoadedModel { get; protected set; }

		/// <summary>
		/// Whether the root is displayed.
		/// </summary>
		public virtual bool Visibility
		{
			get => Root.style.display.value != DisplayStyle.None;
			set => Root.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
		}

		public abstract void Close();

		public bool Equals(IView other)
		{
			return other is not null && other.Id.Equals(Id);
		}
	}

	public abstract class View<TPrefabKey, TModel> : View, IView<TPrefabKey, TModel>
		where TModel : IViewModel<TPrefabKey>
	{
		protected View(VisualElement root, TModel model) : base(root)
		{
			LoadedModel = model;
		}

		public override Guid Id { get; } = Guid.NewGuid();

		public new TModel LoadedModel
		{
			get => (TModel)base.LoadedModel;
			protected set => base.LoadedModel = value;
		}
		IViewModel IView.LoadedModel => LoadedModel;

		/// <summary>
		/// Publish the closed state. The root is removed by the factory, through the view manager.
		/// </summary>
		public override void Close()
		{
			PublishStateChangeEvent(ViewState.Closed);
		}

		/// <summary>
		/// Publish the event of the view state change.
		/// </summary>
		protected abstract void PublishStateChangeEvent(ViewState changedState);
	}
}
