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

		public void ExecuteVillagerActions()
		{
			villagers.ForEach(x => x.MoveToVillagerPanel());
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

		public void RefreshGUI()
		{
			foreach (var view in villagers)
			{
				view.SetHealth(view.Villager.health);
			}
		}

		public void AddRemoveVillagersHealth(int value)
		{
			villagers.ForEach(x => x.Villager.health += value);
		}
	}
}
