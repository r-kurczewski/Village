using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;
using Village.Scriptables;
using Village.Views;

namespace Village.Controllers
{
	[SelectionBase]
	public class GameController : MonoBehaviour
	{
		public const int COUNTRY_A_ENDING_REPUTATION = 600;
		public const int COUNTRY_B_ENDING_REPUTATION = 600;
		public const int NEUTRAL_ENDING_REPUTATION = 400;
		public const float SELL_VALUE_MULTIPLIER = 0.5f;
		public const float TRADE_DISCOUNT = 0.06f;
		public const float STAT_MULTIPIER = 0.2f;
		public const int MAX_HEALTH = 5;
		public const int STAT_MAX = 5;
		public const int RESOURCES_MAX = 999;
		public const int START_VILLAGERS = 6;
		public const int VILLAGER_START_HEALTH = 5;
		public const int MERCHANT_SELL_ITEMS_COUNT = 4;
		public const int MERCHANT_BUY_ITEMS_COUNT = 4;

		public static GameController instance;
		
		public bool autoSave = true;

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
		private TradeController tradeController;

		[SerializeField]
		private GameLog gameLog;

		[SerializeField]
		private GameObject loadingScreen;

		[SerializeField]
		private TipsController hintWindow;

		private AudioController AudioController => AudioController.instance;

		public GameChapter Chapter => turnController.Chapter;

		public bool GameEnds => turnController.GameEnds;

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
			if (SaveController.SaveExists)
			{
				SaveController.SaveData save = SaveController.LoadSaveData();
				LoadPreviousGame(save);
			}
			else
			{
				StartNewGame();
			}
		}

		private void StartNewGame()
		{
			villagerController.CreateStartVillagers(START_VILLAGERS);
			locationController.LoadLoctions();
			resourceController.LoadResources();
			turnController.LoadChapter();
			eventController.LoadChapterEvents();

			UpdateGUI();
			PlayMusic();
			SaveGameState();
		}

		private async void LoadPreviousGame(SaveController.SaveData save)
		{
			loadingScreen.SetActive(true);
			Task assetsLoading = AssetManager.instance.LoadAssets();

			locationController.LoadLoctions();
			turnController.LoadTurnAndChapter(save.turn);
			resourceController.LoadResources(save.resources);
			locationController.LoadBuildings(save.buildings);
			hintWindow.displayedTips = save.displayedHints;
			gameLog.SetLogData(save.log);

			await assetsLoading;

			villagerController.LoadVillagers(save.villagers);
			eventController.LoadChapterEvents(save.chapterEvents);
			eventController.LoadCurrentEvents(save.currentEvents);
			tradeController.LoadTrades(save.merchantTrades);

			UpdateGUI();
			PlayMusic();
			loadingScreen.SetActive(false);
		}

		private void TurnUpdate()
		{
			turnController.ChapterUpdate();
			resourceController.ApplyTurnBonuses();
			eventController.EventUpdate();
			villagerController.VillagerUpdate();
			turnController.CheckIfGameEnds();
			hintWindow.TipUpdate();
			UpdateGUI();
		}

		public void LoadChapterEvents()
		{
			eventController.LoadChapterEvents();
		}

		public void AddRemoveVillagersHealth(int value)
		{
			villagerController.AddRemoveVillagersHealth(value, playSound: true);
		}

		public void AddRemoveResource(Resource resource, int amount)
		{
			resourceController.AddRemoveResource(resource, amount);
		}

		public int GetPredictionFactor()
		{
			return eventController.PredictionFactor;
		}

		public int GetResourceAmount(Resource resource)
		{
			return resourceController.GetResourceAmount(resource);
		}

		public int GetTurnBonus(Resource resource)
		{
			return resourceController.GetTurnBonus(resource);
		}

		public int GetVillagersCount()
		{
			return villagerController.GetVillagersCount();
		}

		public void ApplyIntelligenceBonus()
		{
			villagerController.ApplyIntelligenceBonus();
		}

		public void RegisterTurnBonus(Resource.ResourceAmount resource)
		{
			resourceController.RegisterTurnBonus(resource);
		}

		public bool MerchantAvailable()
		{
			return eventController.MerchantAvailable();
		}

		public void LoadNewMerchantTrades()
		{
			var trades = resourceController.GenerateTrades();
			tradeController.LoadTrades(trades);
		}

		public void LoadTradeWindow(Villager villager)
		{
			tradeController.LoadTradeWindow(villager);
			tradeController.ShowTradeWindow();
		}

		public bool GetTradeActive()
		{
			return tradeController.TradeActive;
		}

		public int GetCurrentTurn()
		{
			return turnController.Turn;
		}

		public void EndTurn()
		{
			StartCoroutine(IEndTurn());
		}

		private IEnumerator IEndTurn()
		{
			turnController.CheckIfGameEnds();
			yield return locationController.IExecuteVillagerActions();
			turnController.MoveToNextTurn();
			TurnUpdate();
			instance.AddLogDayEntry();
			if(autoSave) SaveController.SaveGameState();
		}

		public void SetPredictionFactor()
		{
			eventController.PredictionFactor = 1;

			// load events that would be skipped
			eventController.EventUpdate();
		}

		public void UpdateGUI()
		{
			resourceController.RefreshGUI();
			villagerController.RefreshGUI();
			locationController.RefreshGUI();
			turnController.RefreshGUI();
			eventController.RefreshGUI();
		}

		public void PlayMusic()
		{
			AudioController.PlayMusic();
		}

		public void PlayMusic(AudioClip clip)
		{
			AudioController.PlayMusic(clip);
		}

		public void PlaySound(AudioClip sound)
		{
			AudioController.PlaySound(sound);
		}

		public void SetMusic(AudioClip clip)
		{
			AudioController.SetMusic(clip);
		}

		private static void SaveGameState()
		{
			SaveController.SaveGameState();
		}

		public List<Villager.SaveData> SaveVillagers()
		{
			return villagerController.SaveVillagers();
		}

		public List<Resource.ResourceAmount.SaveData> SaveResources()
		{
			return resourceController.SaveResources();
		}

		public List<string> SaveHints()
		{
			return hintWindow.displayedTips;
		}

		public List<GameEvent.SaveData> SaveCurrentEvents()
		{
			return eventController.SaveCurrentEvents();
		}

		public List<GameEvent.SaveData> SaveChapterEvents()
		{
			return eventController.SaveChapterEvents();
		}

		public List<TradeOffer.SaveData> SaveTrades()
		{
			return tradeController.SaveTrades();
		}

		public List<string> SaveBuildings()
		{
			return locationController.SaveBuildings();
		}

		public void ClearSave()
		{
			SaveController.ClearSave();
		}

		public void TryLoadHint(string hintMessageLocale)
		{
			hintWindow.TryLoadTip(hintMessageLocale);
		}

		public void AddLogSubEntry(GameLog.LogSubEntry subEntry)
		{
			gameLog.UpdateDayEntry(subEntry);
		}

		public void AddLogDayEntry()
		{
			gameLog.PrintDayEntry();
		}
		public List<GameLog.LogEntry> GetGameLogData()
		{
			return gameLog.GetLogData();
		}

		public void LoadGameLogData(List<GameLog.LogEntry> logData)
		{
			gameLog.SetLogData(logData);
		}


	}
}