using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Village.Scriptables;
using Village.Views;
using UnityEngine.Serialization;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using System.Threading.Tasks;
using static Village.Controllers.GameController;

namespace Village.Controllers
{
	[SelectionBase]
	public class VillagerController : MonoBehaviour
	{

		private const string localeVillagerDied = "log/villagerDied";

		[SerializeField]
		private VillagerView villagerPrefab;

		[SerializeField]
		private List<VillagerBase> villagerStartPool;

		public Transform dragParent;

		public List<VillagerView> villagers;

		[SerializeField]
		private LayoutGroup layout;

		public void PutVillager(VillagerView villager)
		{
			villager.transform.SetParent(transform);
			RefreshLayout();
		}

		private void RefreshLayout()
		{
			Canvas.ForceUpdateCanvases();
			layout.enabled = false;
			layout.enabled = true;
		}

		public void MoveVillagersToPanel(bool playSound = false)
		{
			foreach (var villager in villagers)
			{
				villager.MoveToPanel(false);
				PutVillager(villager);
			}
			if(playSound)
			{
				var sound = AudioController.instance.villagerMoveSound;
				AudioController.instance.PlaySound(sound);
			}
		}

		public Villager CreateVillager(VillagerBase villagerBase)
		{
			VillagerView view = Instantiate(villagerPrefab, transform);
			Villager villager = view.GetComponent<Villager>();
			villager.Load(villagerBase);
			view.Load(villager, this);
			villagers.Add(view);
			return villager;
		}

		public Villager CreateVillager(Villager.SaveData data, VillagerBase villagerBase)
		{
			VillagerView view = Instantiate(villagerPrefab, transform);
			Villager villager = view.GetComponent<Villager>();
			villager.Load(data, villagerBase);
			view.Load(villager, this);
			villagers.Add(view);
			return villager;
		}

		public void CreateStartVillagers(int count)
		{
			var randomVillagerBases = villagerStartPool.OrderBy(x => UnityEngine.Random.value).Take(count);
			foreach (var villagerBase in randomVillagerBases)
			{
				var villager = CreateVillager(villagerBase);
				villager.Health = VILLAGER_START_HEALTH;
			}
		}

		public void ApplyIntelligenceBonus()
		{
			villagers.ForEach(x => x.Villager.ApplyIntelligenceBonus());
		}

		public void VillagerUpdate()
		{
			var toRemove = new List<VillagerView>();
			foreach (var view in villagers)
			{
				if (view.Villager.Health == 0)
				{
					toRemove.Add(view);
				}
			}
			foreach (var view in toRemove)
			{
				villagers.Remove(view);
				var entry = new GameLog.LogSubEntry(localeVillagerDied);
				entry.AddParameter("{villager}", view.Villager.villagerBase.villagerName);
				instance.AddLogSubEntry(entry);
				Destroy(view.gameObject);
			}
		}

		public void RefreshGUI()
		{
			villagers.ForEach(x => x.Refresh());
			RefreshLayout();
			
		}

		public void AddRemoveVillagersHealth(int value, bool playSound)
		{
			villagers.ForEach(x => x.Villager.Health += value);
			var sound = AudioController.instance.loseHealthSound;
			if (!instance.GameEnds) AudioController.instance.PlaySound(sound);
		}

		public void LoadVillagers(List<Villager.SaveData> save)
		{
			foreach (var villagerData in save) 
			{
				VillagerBase villagerBase = AssetManager.instance.GetAsset<VillagerBase>(villagerData.villagerName);
				CreateVillager(villagerData, villagerBase);
			}
		}

		public int GetVillagersCount()
		{
			return villagers.Count(x=> x.Villager.Health > 0);
		}

		public void SortVillagers()
		{
			villagers = villagers.OrderBy(x => x.SortIndex).ToList();
		}

		public List<Villager.SaveData> SaveVillagers()
		{
			return villagers.Select(x => x.Villager.Save()).ToList();
		}
	}
}
