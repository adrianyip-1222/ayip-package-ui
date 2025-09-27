using AYip.UI.Samples.BasicExamples.PopUps;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AYip.UI.Samples.BasicExamples.Pages
{
	/// <summary>
	/// This class demonstrates how to create a page with a modal class.
	/// Also, how dependencies are stored and UI elements can be initialized with the data in the modal.
	/// </summary>
	public partial class DemoPage : Page<DemoPage.Modal>
	{
		[SerializeField]
		private TextMeshProUGUI titleLabel;

		[SerializeField]
		private Button closeButton;
		private PageManager _pageManager;
		private PopUpManager _popUpManager;

		public override bool IsProtected { get; protected set; }
		
		public void OnClickNewPage(bool isProtected)
		{
			var newPageModal = new DemoPage.Modal(isProtected, _pageManager, _popUpManager);
			_pageManager.Show(newPageModal);
		}

		public void OnClickNewPopUp()
		{
			var newPopUpModal = new DemoPopUp.Modal();
			_popUpManager.TryShowOrEnqueue(newPopUpModal);
		}
		
		protected override void Initialize()
		{
			_pageManager = LoadedModal.PageManager;
			_popUpManager = LoadedModal.PopUpManager;
			titleLabel.text = LoadedModal.Title;
			IsProtected = LoadedModal.IsProtected;
			closeButton.gameObject.SetActive(LoadedModal.IsCloseButtonEnabled);
		}

		private void OnEnable()
		{
			closeButton.onClick.AddListener(OnClosePage);
		}
		
		private void OnDisable()
		{
			closeButton.onClick.RemoveListener(OnClosePage);
		}

		private void OnClosePage()
		{
			EventListener.NotifyWindowStateChange(this, WindowState.Closed);
		}
	}
}