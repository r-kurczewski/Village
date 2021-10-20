using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapBuilding", menuName ="Village/Location/MapBuilding")]
public class MapBuilding : MapLocation
{
    public string buildDescription;
    public List<ResourceAmount> buildingCost;

    public List<Action> buildingAction;
}
