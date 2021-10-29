using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName ="Message", menuName ="Village/Message")]
	public class Message : ScriptableObject
	{
		public string title;

		public AudioClip music;

		public Color backgroundColor;

		[TextArea(8, 8)]
		public List<string> messages;
	}
}
