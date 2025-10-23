using System;
using UnityEngine;

namespace AYip.UI
{
	public abstract class View : MonoBehaviour, IView
	{
		public abstract Guid Id { get; }
		public GameObject GameObject => gameObject;
		public IViewModel LoadedModel { get; protected set; }
		public bool Visibility { get; set; }
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

		public override void Close()
		{
			PublishStateChangeEvent(ViewState.Closed);
			Destroy(gameObject);
		}
	
		/// <summary>
		/// Publish the event of the view state change.
		/// </summary>
		protected abstract void PublishStateChangeEvent(ViewState changedState);
	}
}