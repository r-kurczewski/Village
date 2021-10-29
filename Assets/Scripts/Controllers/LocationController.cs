using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Village.Views;

namespace Village.Controllers
{
	public class LocationController : MonoBehaviour
	{
		[SerializeField]
		private LocationView[] locations;

		public void LoadLoctions()
		{
			locations = GetComponentsInChildren<LocationView>();
			LocationUpdate();
		}

		public void LocationUpdate()
		{
			foreach (var location in locations)
			{
				location.Reload();
			}
		}

		internal void ExecuteVillagerActions()
		{
			foreach (var location in locations)
			{
				foreach (var slot in location.GetComponentsInChildren<ActionSlot>())
				{
					if (slot.Villager) slot.Action.Execute(slot.Villager);
				}
			}
		}
	}
}