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

namespace Village.Controllers
{
	[SelectionBase]
	public class VillagerController : MonoBehaviour
	{
		[SerializeField]
		private VillagerView villagerPrefab;

		[SerializeField]
		private List<VillagerBase> villagerStartPool;

		public Transform dragParent;

		public List<VillagerView> villagers;

		[SerializeField]
		private LayoutGroup layout;

		[SerializeField]
		private AudioClip loseHealthSound;

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

		public void MoveVillagersToPanel()
		{
			foreach (var villager in villagers)
			{
				villager.MoveToPanel(playSound: false);
				PutVillager(villager);
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
				CreateVillager(villagerBase);
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
					Destroy(view.gameObject);
				}
			}
			foreach (var view in toRemove)
			{
				villagers.Remove(view);
				Destroy(view.gameObject);
			}
		}

		public void RefreshGUI()
		{
			villagers.ForEach(x => x.SetHealth(x.Villager.Health));
			RefreshLayout();
			
		}

		public void AddRemoveVillagersHealth(int value, bool playSound)
		{
			villagers.ForEach(x => x.Villager.Health += value);
			AudioController.instance.PlaySound(loseHealthSound);
		}

		public void LoadVillagers(List<Villager.SaveData> save, Dictionary<string, ScriptableObject> assets)
		{
			foreach (var villagerData in save) 
			{
				VillagerBase villagerBase = assets[villagerData.villagerName] as VillagerBase;
				CreateVillager(villagerData, villagerBase);
			}
		}

		public int GetVillagersCount()
		{
			return villagers.Count;
		}

		public List<Villager.SaveData> SaveVillagers()
		{
			return villagers.Select(x => x.Villager.Save()).ToList();
		}
	}
}
