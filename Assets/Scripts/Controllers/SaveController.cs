using BayatGames.SaveGameFree;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Village.Scriptables.Resource;

namespace Village.Controllers
{
	public class SaveController : MonoBehaviour
	{
		public void SaveState()
		{
			SaveData data = new SaveData();
			data.villagers = GameController.instance.GetVillagers();
			string json = JsonUtility.ToJson(data.villagers[0]);
			SaveGame.Save("data", json);
		}

		public void LoadState()
		{

		}

		private void Save(SaveData data)
		{
			SaveGame.Save("save", data);
		}

		private SaveData Load()
		{
			return SaveGame.Load<SaveData>("save");
		}

		[Serializable]
		public class SaveData
		{
			public int turn;
			public List<ResourceAmount> resources;
			public List<Villager> villagers;
			public List<Event> currentEvents;
			public List<Event> chapterEvents;
		}
	}
}
