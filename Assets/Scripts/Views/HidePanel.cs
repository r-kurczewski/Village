using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.Views
{
	public class HidePanel : MonoBehaviour
	{
		public virtual void ChangeVisibility()
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
