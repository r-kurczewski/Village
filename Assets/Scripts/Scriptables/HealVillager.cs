using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealVillager", menuName = "Village/Effect/HealVillager")]
public class HealVillager : Effect
{
	public override void Apply(Villager villager, int value)
	{
		villager.health += 1;
	}
}
