using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealVillagers", menuName = "Village/Effect/HealVillagers")]
public class HealVillagers : Effect
{
	public override void Apply(Villager villager, int value)
	{
		GameController.instance.AddRemoveVillagerHealth(value);
	}
}
