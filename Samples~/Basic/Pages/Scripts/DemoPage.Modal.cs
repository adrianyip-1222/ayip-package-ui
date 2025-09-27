using AYip.UI.Samples.BasicExamples.PopUps;

namespace AYip.UI.Samples.BasicExamples.Pages
{
	public partial class DemoPage
	{
		/// <summary>
		/// The inner class defining the modal for this page type.
		/// It helps on the encapsulation of the page related data and dependencies.
		/// Of cause, you can also define the modal class outside of this page class.
		/// </summary>
		public class Modal : PageModal
		{
			/// <summary>
			/// The UID of the page instance, used to differentiate multiple instances of the same page prefab.
			/// </summary>
			private static int SpawnIndex;
			
			public Modal(bool isProtected, PageManager pageManager, PopUpManager popUpManager)
			{
				IsProtected = isProtected;
				PageManager = pageManager;
				PopUpManager = popUpManager;
				var isAnyWindowShowing = pageManager.CurrentWindow != null;
				IsCloseButtonEnabled = isAnyWindowShowing;
				Title = $"Page {SpawnIndex++}";
			}

			/// <summary>
			/// A crucial part of the page modal, it defines which prefab to instantiate for this page instance.
			/// <seealso cref="PagePrefabDatabase"/>
			/// </summary>
			public override PageType PrefabKey => PageType.DemoPage;
			
			/// <summary>
			/// The title of this page instance, it shows the instance index in the demo.
			/// </summary>
			public string Title { get; }
			
			/// <summary>
			/// Indicates whether this page is protected (won't be destroyed when stack up)
			/// </summary>
			public bool IsProtected { get; }
			
			/// <summary>
			/// Indicates whether the close button should present.
			/// </summary>
			public bool IsCloseButtonEnabled { get; }
			
			/// <summary>
			/// The dependency of PageManager to open new pages.
			/// </summary>
			public PageManager PageManager { get; }
			
			/// <summary>
			/// The dependency of PopUpManager to open new pop-ups.
			/// </summary>
			public PopUpManager PopUpManager { get; }
		}
	}
}