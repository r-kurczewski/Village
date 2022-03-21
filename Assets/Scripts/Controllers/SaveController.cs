using BayatGames.SaveGameFree;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Village.Scriptables.Resource;
using System.IO;
using static Village.GameLog;

namespace Village.Controllers
{
	public class SaveController : MonoBehaviour
	{
		public static SaveData save;
		private const string saveFileName = "save.dat";

		public static void SaveGameState()
		{
			var gController = GameController.instance;
			SaveData data = new SaveData
			{
				turn = gController.GetCurrentTurn(),
				villagers = gController.SaveVillagers(),
				resources = gController.SaveResources(),
				currentEvents = gController.SaveCurrentEvents(),
				chapterEvents = gController.SaveChapterEvents(),
				merchantTrades = gController.SaveTrades(),
				buildings = gController.SaveBuildings(),
				log = gController.GetGameLogData(),
			};
			SaveGame.Save(saveFileName, data);
		}

		public static SaveData LoadSaveData()
		{
			save = SaveGame.Load<SaveData>(saveFileName);
			if (save == null)
			{
				throw new InvalidDataException("There was a problem with loading save.");
			}
			else return save;
		}

		public static bool SaveExists => SaveGame.Exists(saveFileName);

		public static void ClearSave()
		{
			save = null;
			SaveGame.Delete(saveFileName);
		}

		[Serializable]
		public class SaveData
		{
			public int turn;
			public List<ResourceAmount.SaveData> resources;
			public List<Villager.SaveData> villagers;
			public List<GameEvent.SaveData> currentEvents;
			public List<GameEvent.SaveData> chapterEvents;
			public List<TradeOffer.SaveData> merchantTrades;
			public List<string> buildings;
			public List<LogEntry> log;
		}
	}
}
