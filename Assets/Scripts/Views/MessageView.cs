using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Village.Scriptables;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using Lean.Localization;
using Village.Controllers;

namespace Village.Views
{
	public class MessageView : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private string localeLoading;

		[SerializeField]
		private Image background;

		[SerializeField]
		private TMP_Text titleField;

		[SerializeField]
		private TMP_Text messageField;

		[SerializeField]
		private TMP_Text clickHint;

		[SerializeField]
		private Message data;

		private int currentMessage;

		[SerializeField]
		public UnityEvent MessagesEnded;

		private void Start()
		{
			if (data) LoadMessage(data);
		}

		public void LoadMessage(Message data)
		{
			if (data != null)
			{
				currentMessage = 0;
				this.data = data;
				if (data.music)
				{
					AudioController.instance.PlayMusic(data.music);
				}
				background.color = data.backgroundColor;
				Reload();
			}
			else Debug.LogWarning("No message loaded!");
		}

		private void Reload()
		{
			if (currentMessage < data.MessageCount)
			{
				messageField.text = data.GetMessage(currentMessage);
				titleField.text = data.Title;
			}
			else
			{
				MessagesEnded.Invoke();
			}
		}

		public void ShowLoadingMessage()
		{
			clickHint.text = LeanLocalization.GetTranslationText(localeLoading);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			currentMessage++;
			Reload();
		}
	}
}