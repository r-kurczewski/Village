using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.Views
{
	public class HelpPanel : MonoBehaviour
	{
		public void ChangeVisibility()
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}
	}
}
