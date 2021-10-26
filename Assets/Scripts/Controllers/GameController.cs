using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using Village.Views;
using Village.Scriptables;

namespace Village.Controllers
{
	[SelectionBase]
	public class GameController : MonoBehaviour
	{
		public static GameController instance;

		[SerializeField]
		private ResourceController resourceController;

		[SerializeField]
		private VillagerController villagerController;

		[SerializeField]
		private EventController eventController;

		[SerializeField]
		private TurnController turnController;

		[SerializeField]
		private AudioSource audioPlayer;

		public GameChapter Chapter => turnController.Chapter;

		public int Turn => turnController.Turn;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else Debug.LogWarning("There is more than one GameController in the scene!");
		}
		private void Start()
		{
			villagerController.CreateStartVillagers(6);
			eventController.LoadChapterEvents();
			resourceController.LoadResources();
			TurnUpdate();
		}

		private void TurnUpdate()
		{
			turnController.ChapterUpdate();
			eventController.EventUpdate();
			UpdateGUI();
		}

		public void AddRemoveVillagersHealth(int value)
		{
			villagerController.AddRemoveVillagersHealth(value);
		}
		public void AddRemoveResource(Resource resource, int amount)
		{
			resourceController.AddRemoveResource(resource, amount);
		}

		public int GetResourceAmount(Resource resource)
		{
			return resourceController.GetResourceAmount(resource);
		}

		public int GetCurrentTurn()
		{
			return turnController.Turn;
		}

		public void EndTurn()
		{
			turnController.CheckIfGameEnds();
			villagerController.ExecuteVillagerActions();
			turnController.MoveToNextTurn();
			TurnUpdate();
		}

		public void SetPredictionFactor(int value)
		{
			eventController.PredictionFactor = value;
		}

		public void UpdateGUI()
		{
			resourceController.RefreshGUI();
			villagerController.RefreshGUI();
			turnController.RefreshGUI();
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
	}
}