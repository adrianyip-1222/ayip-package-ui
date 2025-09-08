using VContainer;
using Base = AYip.UI; 

namespace AYip.UI.VConatiner
{
	/// <summary>
	/// An example of how to use VContainer to inject window modal 
	/// </summary>
	public abstract class Window<TPrefabKey, TWindow, TModal> : Base.Window<TPrefabKey, TWindow, TModal>
		where TWindow : IWindow<TPrefabKey, TWindow, TModal>
		where TModal : IWindowModal<TPrefabKey>
	{
		[Inject]
		private void Construct(TModal baseModal)
		{
			LoadedModal = baseModal;
		}
	}
}