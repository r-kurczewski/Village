using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "HealVillagers", menuName = "Village/Effect/HealVillagers")]
	public class HealVillagers : Effect
	{
		public override void Apply(int value, Villager villager)
		{
			GameController.instance.AddRemoveVillagersHealth(value);
		}
	}
}