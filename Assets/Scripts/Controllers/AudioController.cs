using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.Controllers
{
	public class AudioController : MonoBehaviour
	{
		public static AudioController instance;
		private AudioSource audioPlayer;

		private void Awake()
		{
			if (instance != null)
			{
				Destroy(gameObject);
				return;
			}
			instance = this;

			audioPlayer = GetComponent<AudioSource>();

			DontDestroyOnLoad(gameObject);
		}

		public void PlayMusic(AudioClip clip)
		{
			audioPlayer.clip = clip;
			audioPlayer.Play();
		}

		public void PlaySound(AudioClip sound)
		{
			audioPlayer.PlayOneShot(sound);
		}

		public void PlayMusicIfChanged(AudioClip music)
		{
			if(audioPlayer.clip != music)
			{
				PlayMusic(music);
			}
		}
	}
}
