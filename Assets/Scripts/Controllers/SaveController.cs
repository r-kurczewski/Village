using BayatGames.SaveGameFree;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Village.Scriptables.Resource;
using System.IO;
using static Village.GameLog;
using static Village.Controllers.GameController;

namespace Village.Controllers
{
	public class SaveController : MonoBehaviour
	{
		private const string saveFileName = "save.dat";

		public static SaveData save;

		public static void SaveGameState()
		{
			SaveData data = new SaveData
			{
				difficulty = instance.SaveDifficulty(),
				turn = instance.GetCurrentTurn(),
				villagers = instance.SaveVillagers(),
				resources = instance.SaveResources(),
				currentEvents = instance.SaveCurrentEvents(),
				chapterEvents = instance.SaveChapterEvents(),
				merchantTrades = instance.SaveTrades(),
				buildings = instance.SaveBuildings(),
				displayedHints = instance.SaveHints(),
				log = instance.GetGameLogData(),
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
			public GameDifficulty difficulty;
			public int turn;
			public List<ResourceAmount.SaveData> resources;
			public List<Villager.SaveData> villagers;
			public List<GameEvent.SaveData> currentEvents;
			public List<GameEvent.SaveData> chapterEvents;
			public List<TradeOffer.SaveData> merchantTrades;
			public List<string> buildings;
			public List<string> displayedHints;
			public List<LogEntry> log;
		}
	}
}
