using System;
using UnityEngine;

namespace AYip.UI.UGUI
{
	/// <summary>
	/// The base of a uGUI view: a MonoBehaviour on a prefab spawned under a RectTransform.
	/// </summary>
	public abstract class View : MonoBehaviour, IView
	{
		public abstract Guid Id { get; }
		public IViewModel LoadedModel { get; protected set; }

		/// <summary>
		/// Whether the GameObject of the view is active.
		/// </summary>
		public virtual bool Visibility
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
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
		public override Guid Id { get; } = Guid.NewGuid();

		public new TModel LoadedModel
		{
			get => (TModel)base.LoadedModel;
			protected set => base.LoadedModel = value;
		}
		IViewModel IView.LoadedModel => LoadedModel;

		/// <summary>
		/// Publish the closed state. The GameObject is destroyed by the factory, through the view manager.
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
