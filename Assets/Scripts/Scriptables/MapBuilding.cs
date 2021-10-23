using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MapBuilding", menuName ="Village/Location/MapBuilding")]
public class MapBuilding : MapLocation
{
    public string buildActionName;
    public List<ResourceAmount> buildingCost = new List<ResourceAmount>();

    public List<Action> buildingAction;

    public virtual void ApplyOnetimeBonus()
	{

	}

    public virtual void ApplyTurnBonus()
	{

	}
}
