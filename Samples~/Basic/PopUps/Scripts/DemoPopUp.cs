using TMPro;
using UnityEngine;

namespace AYip.UI.Samples.BasicExamples.PopUps
{
	/// <summary>
	/// This class demonstrates how to create a pop-up with a modal class.
	/// Also, how dependencies are stored and UI elements can be initialized with the data in the modal.
	/// </summary>
	public class DemoPopUp : PopUp<DemoPopUp.Modal>
	{
		[SerializeField]
		private TextMeshProUGUI titleLabel;
		
		private void Awake()
		{
			titleLabel.text = LoadedModal.Title;
		}
		
		/// <summary>
		/// The inner class defining the modal for this pop-up type.
		/// It helps on the encapsulation of the pop-up related data and dependencies.
		/// Of cause, you can also define the modal class outside of this pop-up class.
		/// </summary>
		public class Modal : PopUpModal
		{
			public Modal()
			{
				Title = $"PopUp {SpawnIndex++}";
			}

			/// <summary>
			/// The UID of the pop-up instance, used to differentiate multiple instances of the same pop-up prefab.
			/// </summary>
			private static int SpawnIndex;
			
			public override PopUpType PrefabKey => PopUpType.DemoPopUp;
			public string Title { get; }
		}
	}
}