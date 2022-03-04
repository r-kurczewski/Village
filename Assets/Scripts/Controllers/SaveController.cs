using BayatGames.SaveGameFree;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Village.Scriptables.Resource;
using Village.Views;
using System.IO;
using BayatGames.SaveGameFree.Serializers;
using System.Runtime.Serialization;

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
				predictionFactor = gController.GetPredictionFactor(),
				villagers = gController.SaveVillagers(),
				resources = gController.SaveResources(),
				currentEvents = gController.SaveCurrentEvents(),
				chapterEvents = gController.SaveChapterEvents(),
				merchantTrades = gController.SaveTrades(),
				buildings = gController.SaveBuildings()
			};
			SaveGame.Save(saveFileName, data);
			Debug.Log("Saving state...");
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
