using UnityEngine;
using UnityEngine.UI;
using Village.Controllers;

namespace Village
{
	public class GameMainMenu : MonoBehaviour
	{
		[SerializeField]
		private Button loadButton;

		[SerializeField]
		private Button exitButton;

		void Start()
		{
			loadButton.interactable = SaveController.IsCorrectSave;

			#if UNITY_WEBGL
				exitButton.interactable = false;
			#endif
		}
	}
}
