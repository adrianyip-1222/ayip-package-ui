using AYip.UI.Samples.BasicExamples.Pages;
using AYip.UI.Samples.BasicExamples.PopUps;
using UnityEngine;

namespace AYip.UI.Samples.BasicExamples
{
	/// <summary>
	/// This bootstrap script demonstrates how to initialize the UI framework.
	/// </summary>
	public class Bootstrap : MonoBehaviour
	{
		[Header("Pop Up")]
		[SerializeField]
		private RectTransform defaultPopUpCanvasRoot;
		
		[SerializeField]
		private PopUpPrefabDatabase popUpPrefabDatabase;

		[Header ("Page")]
		[SerializeField]
		private RectTransform defaultPageCanvasRoot;
		
		[SerializeField]
		private PagePrefabDatabase pagePrefabDatabase;
		
		private WindowStateEventListener _windowStateEventListener;
		private PopUpFactory _popUpFactory;
		private PageFactory _pageFactory;

		#region Properties
		/// <summary>
		/// The pop up manager instance.
		/// </summary>
		public PopUpManager PopUpManager { get; private set; }
		public PageManager PageManager { get; private set; }
		#endregion
		
		private void Awake()
		{
			// Initialize the window state event listener.
			_windowStateEventListener = new WindowStateEventListener();
			
			// Initialize the pop-up factory and manager.
			_popUpFactory = new PopUpFactory(popUpPrefabDatabase, _windowStateEventListener);
			PopUpManager = new PopUpManager(defaultPopUpCanvasRoot, _windowStateEventListener, _popUpFactory);
			
			// Initialize the page factory and manager.
			_pageFactory = new PageFactory(pagePrefabDatabase, _windowStateEventListener);
			PageManager = new PageManager(defaultPageCanvasRoot, _windowStateEventListener, _pageFactory);
		}

		private void Start()
		{
			var demoPageModal = new DemoPage.Modal(
				isProtected: false,
				pageManager: PageManager,
				popUpManager: PopUpManager
			);

			PageManager.Show(demoPageModal);
		}

		private void OnDestroy()
		{
			// Manually dispose of all created instances.
			_windowStateEventListener?.Dispose();
			
			_popUpFactory?.Dispose();
			PopUpManager?.Dispose();
			
			_pageFactory?.Dispose();
			PageManager?.Dispose();
		}
	}
}