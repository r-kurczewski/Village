using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Village.Scriptables;

namespace Village.Views
{
	public class LocationView : MonoBehaviour
	{
		[SerializeField]
		private MapLocation location;

		[SerializeField]
		private bool isBuilt = false;

		[SerializeField]
		private ActionSlot actionSlotPrefab;

		[SerializeField]
		private Action buildBaseAction;

		public MapLocation Location => location;

		[SerializeField]
		private List<ActionSlot> _slots;

		public List<ActionSlot> ActionSlots => _slots;

		public bool Built => isBuilt;

		private void Clear()
		{
			foreach (var slot in _slots)
			{
				Destroy(slot.gameObject);
			}
			_slots.Clear();
		}

		public void Load()
		{
			Load(location);
		}

		public void Load(MapLocation location)
		{
			LoadBasicActions();

			if (location is MapBuilding building)
			{
				LoadBuildingActions(building);
				bool noActions = building.basicActions.Count == 0 && building.buildingAction.Count == 0;
				if (isBuilt && noActions)
				{
					SetVisibility(false);
				}
			}
		}

		private void LoadBuildingActions(MapBuilding building)
		{
			if (isBuilt)
			{
				foreach (var action in building.buildingAction)
				{
					LoadAction(action);
				}
			}
			else
			{
				var buildAction = new BuildAction(buildBaseAction, building, this);
				LoadAction(buildAction);
			}
		}

		private void LoadBasicActions()
		{
			if (location.basicActions == null) return;

			foreach (var action in location.basicActions)
			{
				LoadAction(action);
			}
		}

		private void LoadAction(IAction action)
		{
			var actionSlot = Instantiate(actionSlotPrefab, transform);
			_slots.Add(actionSlot);
			actionSlot.Load(action);
		}

		public void SetAsBuilt()
		{
			isBuilt = true;
		}

		public void SetVisibility(bool visibility)
		{
			gameObject.SetActive(visibility);
		}

		public void Reload()
		{
			Clear();
			Load(location);
		}
	}
}