using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Village.Scriptables;
using Village.Views;
using static Village.Controllers.GameController;

namespace Village.Controllers
{
	public class LocationController : MonoBehaviour
	{
		[SerializeField]
		private LocationView[] locations;

		[SerializeField]
		private LocationView merchantLocation;

		public List<ActionSlot> actionSlots;

		public void LoadLoctions()
		{
			locations = GetComponentsInChildren<LocationView>();
			foreach (var location in locations)
			{
				location.Load();
			}
			actionSlots = locations
				.SelectMany(x => x.GetComponentsInChildren<ActionSlot>())
				.OrderByDescending(x => x.Action.ExecutionPriority)
				.ToList();
			RefreshGUI();
		}

		public void RefreshGUI()
		{
			merchantLocation.SetVisibility(instance.MerchantAvailable());
			actionSlots = locations
				.SelectMany(x => x.ActionSlots)
				.OrderByDescending(x => x.Action.ExecutionPriority)
				.ToList();
		}

		public IEnumerator IExecuteVillagerActions()
		{
			foreach (var actionSlot in actionSlots)
			{
				if (actionSlot.Villager)
				{
					yield return actionSlot.Action.Execute(actionSlot.Villager);
				}
			}
		}

		public List<string> SaveBuildings()
		{
			List<string> buildings = new List<string>();
			foreach (var view in locations)
			{
				if (view.Location is MapBuilding building)
				{
					if (view.Built) buildings.Add(building.name);
				}
			}
			return buildings;
		}

		public void LoadBuildings(List<string> save)
		{
			foreach (var view in locations)
			{
				if (view.Location is MapBuilding building)
				{
					if (save.Contains(building.name))
					{
						view.Build();
					}
				}
			}
		}
	}
}