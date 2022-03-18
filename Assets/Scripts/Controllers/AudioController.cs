using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

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

		[Header("Sounds")]
		public AudioClip villagerMoveSound;
		public AudioClip actionPutSound;
		public AudioClip loseHealthSound;
		public AudioClip newTurnSound;
		public AudioClip newEventSound;

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
			SetStartVolumes();
		}

		private void SetStartVolumes()
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

		public void PlayMusicIfChanged(AudioClip clip)
		{
			if(music.clip != clip)
			{
				PlayMusic(clip);
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
