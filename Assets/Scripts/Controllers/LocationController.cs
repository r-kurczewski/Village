using System;
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

		public void LoadLoctions()
		{
			locations = GetComponentsInChildren<LocationView>();
			RefreshGUI();
		}

		public void RefreshGUI()
		{
			foreach (var location in locations)
			{
				location.Reload();
			}
			merchantLocation.SetVisibility(instance.MerchantAvailable());
		}

		public void ExecuteVillagerActions()
		{
			foreach (var location in locations)
			{
				foreach (var slot in location.GetComponentsInChildren<ActionSlot>())
				{
					if (slot.Villager) slot.Action.Execute(slot.Villager);
				}
			}
		}

		public void ApplyTurnBonuses()
		{
			foreach (var view in locations)
			{
				if (view.Location is MapBuilding building)
				{
					if (view.Built) building.ApplyTurnBonus();
				}
			}
		}

		public List<string> SaveBuildings()
		{
			List<string> buildings = new List<string>();
			foreach(var view in locations)
			{
				if(view.Location is MapBuilding building)
				{
					if (view.Built) buildings.Add(building.name);
				}
			}
			return buildings;
		}

		public void LoadBuildings(List<string> save)
		{
			foreach(var view in locations)
			{
				if(view.Location is MapBuilding building)
				{
					if (save.Contains(building.name)) view.SetAsBuilt();
				}
			}
		}
	}
}