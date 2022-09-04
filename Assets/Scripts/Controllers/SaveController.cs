﻿using System;
using System.Collections.Generic;
using UnityEngine;
using static Village.Controllers.GameController;
using static Village.Controllers.LogController;
using static Village.Scriptables.Resource;

namespace Village.Controllers
{
	public class SaveController : MonoBehaviour
	{
		private static readonly string saveFullPath = $"{Application.persistentDataPath}/save.dat";

		public static SaveData save;

		public static bool Encryption { get; internal set; }

		public static bool IsCorrectSave
		{
			get
			{
				bool isCorrectSave = false;
				try
				{
					SaveManager.Instance.Load<SaveData>(saveFullPath, CheckSaveIfCorrect, Encryption);
					return isCorrectSave;
				}
				catch(Exception ex)
				{
					Debug.LogError($"There is a problem with loading a save: {ex.Message}");
					return false;
				}

				void CheckSaveIfCorrect(SaveData data, SaveResult result, string message)
				{
					isCorrectSave = result is SaveResult.Success;
				}
			}
		}

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
			SaveManager.Instance.Save(data, saveFullPath, OnSaveCompleted, Encryption);
		}

		public static void OnSaveCompleted(SaveResult result, string message)
		{
			if (result is SaveResult.Error)
			{
				Debug.LogError($"Could not save a game: {message}");
				SaveManager.Instance.ClearFIle(saveFullPath);
			}
		}

		public static SaveData LoadSaveData()
		{
			SaveManager.Instance.Load<SaveData>(saveFullPath, OnLoadCompleted, Encryption);
			return save;
		}

		public static void OnLoadCompleted(SaveData data, SaveResult result, string message)
		{
			if (result is SaveResult.Success)
			{
				Debug.Log($"Save loaded: {message}");
				save = data;
			}
			else if (result is SaveResult.EmptyData)
			{
				Debug.Log($"No save to load: {message}");
				save = null;
			}
			else if (result is SaveResult.Error)
			{
				Debug.LogError($"There was a problem with loading save: {message}");
				save = null;
			}
		}

		public static void ClearSave()
		{
			save = null;
			SaveManager.Instance.ClearFIle(saveFullPath);
			Debug.Log("Save cleared.");
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
