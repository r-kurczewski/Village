using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Village.Controllers;

namespace Village
{
	public class LoadGameButton : MonoBehaviour
	{
		[SerializeField]
		private Button button;

		void Start()
		{
			button.interactable = SaveController.SaveExists;
		}
	}
}
