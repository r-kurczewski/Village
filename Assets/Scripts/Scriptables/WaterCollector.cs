using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Water Collector", menuName = "Village/Location/Water Collector")]
public class WaterCollector : MapBuilding
{
	public override void ApplyTurnBonus()
	{
		Debug.LogWarning("Water collector!");
	}
}
