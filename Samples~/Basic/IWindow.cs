using Package = AYip.UI; 

namespace AYip.UI.Samples.BasicExamples
{
	public interface IWindow : Package.IWindow
	{
		/// <summary>
		/// Inject the modal to the window.
		/// If you use VContainer by importing the VContainer support from package sample page.
		/// The initialization can be done by the windows' Awake() or Start() since the LoadedModal will be injected instead.
		/// </summary>
		void InjectModal(IWindowModal modal);
	}
}