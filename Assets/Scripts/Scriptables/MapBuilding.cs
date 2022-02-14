using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Village.Scriptables.Resource;
using UnityEngine.Serialization;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "MapBuilding", menuName = "Village/Location/MapBuilding")]
	public class MapBuilding : MapLocation
	{
		public string LocaleBuildActionName;

		[TextArea(4,5), FormerlySerializedAs("buildingDescription")]
		public string localeBuildingDescription;

		public List<ResourceAmount> buildingCost = new List<ResourceAmount>();

		public List<Action> buildingAction;

		public virtual void ApplyOnetimeBonus()
		{

		}

		public virtual void ApplyTurnBonus()
		{

		}
	}
}