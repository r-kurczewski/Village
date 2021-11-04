using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Village.Scriptables;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Village.Views
{
	public class MessageView : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private Image background;

		[SerializeField]
		private TMP_Text titleField;

		[SerializeField]
		private TMP_Text messageField;

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
				var audio = FindObjectOfType<AudioSource>();
				if (data.music)
				{
					audio.clip = data.music;
					audio.Play();
				}
				background.color = data.backgroundColor;
				Reload();
			}
			else Debug.LogWarning("No message loaded!");
		}

		private void Reload()
		{
			if (currentMessage < data.messages.Count)
			{
				messageField.text = data.messages[currentMessage];
				titleField.text = data.title;
			}
			else
			{
				MessagesEnded.Invoke();
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			currentMessage++;
			Reload();
		}
	}
}