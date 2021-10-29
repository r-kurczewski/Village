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

		public void Start()
		{
			Clear();
			if (location)
			{
				Load(location);
			}
			else Debug.LogWarning("No location set.", this);
		}

		private void Clear()
		{
			foreach (Transform obj in transform)
			{
				Destroy(obj.gameObject);
			}
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
					Hide();
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
			actionSlot.Load(action);
		}

		public void Build()
		{
			if (location is MapBuilding building)
			{
				building.ApplyOnetimeBonus();
				isBuilt = true;
			}
			else Debug.LogWarning("Trying to build not-building location!", this);
		}

		private void Show()
		{
			gameObject.SetActive(true);
		}

		private void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Reload()
		{
			Clear();
			Load(location);
		}
	}
}