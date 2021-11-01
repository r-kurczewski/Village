using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Village.Scriptables;
using Village.Views;
using UnityEngine.Serialization;

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

		public void PutVillager(VillagerView villager)
		{
			villager.transform.SetParent(transform);
		}

		public void MoveVillagersToPanel()
		{
			foreach (var villager in villagers)
			{
				villager.MoveToPanel();
				PutVillager(villager);
			}
		}

		public Villager CreateNewVillager(VillagerBase villagerBase)
		{
			VillagerView view = Instantiate(villagerPrefab, transform);
			Villager villager = view.GetComponent<Villager>();
			villager.Load(villagerBase);
			view.Load(villager, this);
			villagers.Add(view);
			return villager;
		}

		public void CreateStartVillagers(int count)
		{
			var randomVillagerBases = villagerStartPool.OrderBy(x => UnityEngine.Random.value).Take(count);
			foreach (var villagerBase in randomVillagerBases)
			{
				CreateNewVillager(villagerBase);
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
		}

		public void AddRemoveVillagersHealth(int value)
		{
			villagers.ForEach(x => x.Villager.Health += value);
		}

		public int GetVillagersCount()
		{
			return villagers.Count;
		}
	}
}
