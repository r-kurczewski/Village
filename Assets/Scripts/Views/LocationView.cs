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

		private ActionSlot buildActionSlot;

		public void Load()
		{
			Load(location);
		}

		public void Load(MapLocation location)
		{
			LoadBasicActions();

			if (location is MapBuilding building)
			{
				if (isBuilt)
				{
					LoadBuildingActions(building);
				}
				else
				{
					LoadBuildAction(building);
				}

				Refresh(building);
			}
		}

		private void Refresh(MapBuilding building)
		{
			bool noActions = building.basicActions.Count == 0 && building.buildingAction.Count == 0;
			if (isBuilt && noActions)
			{
				SetVisibility(false);
			}
		}

		private void LoadBuildingActions(MapBuilding building)
		{
			foreach (var action in building.buildingAction)
			{
				LoadAction(action);
			}
		}

		private void LoadBuildAction(MapBuilding building)
		{
			var buildAction = new BuildAction(buildBaseAction, building, this);
			buildActionSlot = LoadAction(buildAction);
		}

		private void LoadBasicActions()
		{
			if (location.basicActions == null) return;

			foreach (var action in location.basicActions)
			{
				LoadAction(action);
			}
		}

		private ActionSlot LoadAction(IAction action)
		{
			var actionSlot = Instantiate(actionSlotPrefab, transform);
			_slots.Add(actionSlot);
			actionSlot.Load(action);
			return actionSlot;
		}

		public void Build()
		{
			isBuilt = true;
			buildActionSlot.gameObject.SetActive(false);
			if (location is MapBuilding building)
			{
				buildActionSlot.RemoveVillager(false);
				LoadBuildingActions(building);
				building.ApplyOnetimeBonus();
				Refresh(building);
			}
		}

		public void SetVisibility(bool visible)
		{
			if (!visible) ActionSlots.ForEach(x => x.RemoveVillager(false));
			gameObject.SetActive(visible);
		}
	}
}