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

		public const float STAT_MULTIPIER = 0.2f;
		public const int STAT_MAX = 5;
		public const int HEALTH_MAX = 4;
		public const int COUNTRY_A_ENDING_REPUTATION = 700;
		public const int COUNTRY_B_ENDING_REPUTATION = 700;
		public const int NEUTRAL_ENDING_REPUTATION = 500;

		[SerializeField]
		private ResourceController resourceController;

		[SerializeField]
		private VillagerController villagerController;

		[SerializeField]
		private LocationController locationController;

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
			locationController.LoadLoctions();
			eventController.LoadChapterEvents();
			resourceController.LoadResources();
			TurnUpdate();
		}

		private void TurnUpdate()
		{
			turnController.ChapterUpdate();
			eventController.EventUpdate();
			locationController.LocationUpdate();
			villagerController.VillagerUpdate();
			turnController.CheckIfGameEnds();
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

		public int GetVillagersCount()
		{
			return villagerController.GetVillagersCount();
		}

		public int GetCurrentTurn()
		{
			return turnController.Turn;
		}

		public void EndTurn()
		{
			turnController.CheckIfGameEnds();
			locationController.ExecuteVillagerActions();
			villagerController.MoveVillagersToPanel();
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