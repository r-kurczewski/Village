using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "HealVillager", menuName = "Village/Effect/HealVillager")]
	public class HealVillager : Effect
	{
		public override void Apply(int value, Villager villager)
		{
			villager.health += 1;
		}
	}
}