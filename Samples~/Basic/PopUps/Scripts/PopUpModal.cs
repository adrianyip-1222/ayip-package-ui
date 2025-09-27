using AYip.Foundation;
using AYip.UI.PopUps;

namespace AYip.UI.Samples.BasicExamples.PopUps
{
	public abstract class PopUpModal : DisposableBase, IPopUpModal<PopUpType>
	{
		/// <summary>
		/// The prefab key for instantiating the pop-up.
		/// </summary>
		public abstract PopUpType PrefabKey { get; }
		object IWindowModal.PrefabKey => PrefabKey;
	}
}