using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Village.Controllers;

namespace Village
{ 
	public class MusicOnStart : MonoBehaviour
	{
		[SerializeField]
		private AudioClip music;

		private void Start()
		{
			AudioController.instance.PlayMusicIfChanged(music);
		}
	}
}
