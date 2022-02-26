using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Village.Controllers
{
	public class AudioController : MonoBehaviour
	{
		private const string masterVolumeString = "masterVol";
		private const string musicVolumeString = "musicVol";
		private const string soundEffectsVolumeString = "effectsVol";

		public static AudioController instance;

		[SerializeField]
		private AudioMixer mixer;
		
		[SerializeField]
		private AudioSource music;

		[SerializeField]
		private AudioSource soundEffects;
		
		private bool blockSound;

		private float LinearToVolume(float value) => (float)Math.Log10(value) * 20;

		private void Awake()
		{
			if (instance != null)
			{
				Destroy(gameObject);
				return;
			}
			else instance = this;

			DontDestroyOnLoad(gameObject);
		}

		private void Start()
		{
			SetMasterVolume(GameSettings.MasterVolume);
			SetMusicVolume(GameSettings.MusicVolume);
			SetEffectsVolume(GameSettings.EffectsVolume);
		}

		public void PlayMusic(AudioClip clip)
		{
			music.clip = clip;
			music.Play();
		}

		public void PlaySound(AudioClip sound)
		{
			if (blockSound)
			{
				blockSound = false;
				return;
			}
			soundEffects.PlayOneShot(sound);
		}

		public void PlayMusicIfChanged(AudioClip music)
		{
			if(this.music.clip != music)
			{
				PlayMusic(music);
			}
		}

		public void SetMusic(AudioClip clip)
		{
			music.clip = clip;
		}

		public void PlayMusic()
		{
			music.Play();
		}

		public void StopMusic()
		{
			music.Stop();
		}

		public void SetMasterVolume(float volume)
		{
			mixer.SetFloat(masterVolumeString, LinearToVolume(volume));
		}

		public void SetMusicVolume(float volume)
		{
			mixer.SetFloat(musicVolumeString, LinearToVolume(volume));
		}

		public void SetEffectsVolume(float volume)
		{
			mixer.SetFloat(soundEffectsVolumeString, LinearToVolume(volume));
		}
	}
}
