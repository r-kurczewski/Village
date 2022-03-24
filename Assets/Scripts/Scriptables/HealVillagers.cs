using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Village.Controllers.GameController;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "HealVillagers", menuName = "Village/Effect/HealVillagers")]
	public class HealVillagers : Effect
	{
		private const string treatmentTipLocale = "tips/treatment";

		public override void Apply(int value, Villager villager)
		{
			instance.AddRemoveVillagersHealth(value);
			if (value < 0) instance.TryLoadHint(treatmentTipLocale);
		}
	}
}