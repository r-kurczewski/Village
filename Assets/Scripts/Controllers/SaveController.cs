using BayatGames.SaveGameFree;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Village.Scriptables.Resource;
using Village.Views;
using System.IO;

namespace Village.Controllers
{
	public class SaveController : MonoBehaviour
	{
		public static SaveData save;
		private const string saveFileName = "save.dat";

		public static void SaveGameState()
		{
			var gController = GameController.instance;
			SaveData data = new SaveData();
			data.turn = gController.GetCurrentTurn();
			data.predictionFactor = gController.GetPredictionFactor();
			data.villagers = gController.SaveVillagers();
			data.resources = gController.SaveResources();
			data.currentEvents = gController.SaveCurrentEvents();
			data.chapterEvents = gController.SaveChapterEvents();
			data.merchantTrades = gController.SaveTrades();
			data.buildings = gController.SaveBuildings();
			SaveGame.Save(saveFileName, data);
			Debug.Log("Saving state...");
		}

		public static void LoadSaveData()
		{
			save = SaveGame.Load<SaveData>(saveFileName);
			Debug.Log("Loaded save.");
		}

		public static bool SaveExists => SaveGame.Exists(saveFileName);

		public static void ClearSave()
		{
			Debug.Log("Erasing save file...");
			save = null;
			SaveGame.Delete(saveFileName);
		}

		[Serializable]
		public class SaveData
		{
			public int turn;
			public int predictionFactor;
			public List<ResourceAmount.SaveData> resources;
			public List<Villager.SaveData> villagers;
			public List<GameEvent.SaveData> currentEvents;
			public List<GameEvent.SaveData> chapterEvents;
			public List<TradeOffer.SaveData> merchantTrades;
			public List<string> buildings;
		}

	}
}
