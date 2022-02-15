using Lean.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName ="Message", menuName ="Village/Message")]
	public class Message : ScriptableObject
	{
		[SerializeField]
		private string localeTitle;

		public AudioClip music;

		public Color backgroundColor;

		[SerializeField]
		private List<string> localeMessages;

		public string Title => LeanLocalization.GetTranslationText(localeTitle);

		public string GetMessage(int i) => LeanLocalization.GetTranslationText(localeMessages[i]);

		public int MessageCount => localeMessages.Count;
	}
}
