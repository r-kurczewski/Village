using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;
using static Village.Controllers.GameController;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "ResourceAction", menuName = "Village/Action/ResourceAction")]
	public class ResourceAction : Action
	{
		public override IEnumerator Execute(Villager target)
		{
			float multiplier = GetMultiplier(target);
			if (IsCostCorrect())
			{
				ApplyCosts();
				ApplyEffects(multiplier);
			}
			yield break;
		}

		public override float GetMultiplier(Villager villager)
		{
			if (villager is null) return 1;

			int stat1Val = 0, stat2Val = 0;
			if (stat1) stat1Val = villager.GetEffectiveStatValue(stat1);
			if (stat2) stat2Val = villager.GetEffectiveStatValue(stat2);
			return 1 + (stat1Val + stat2Val) * STAT_MULTIPIER;
		}
}

}