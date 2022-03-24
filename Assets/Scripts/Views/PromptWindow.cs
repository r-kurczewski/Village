using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Village.Views
{
	public class PromptWindow : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text label;

		[HideInInspector]
		public UnityEvent OnAccept;

		[HideInInspector]
		public UnityEvent OnDecline;

		public void LoadMessage(string text)
		{
			label.text = text;
		}

		public void Close()
		{
			Destroy(gameObject);
		}

		public void Accept()
		{
			OnAccept.Invoke();
		}

		public void Decline()
		{
			OnDecline.Invoke();
		}
	}
}