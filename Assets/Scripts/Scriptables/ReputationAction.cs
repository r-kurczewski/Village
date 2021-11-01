using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;
using static Village.Controllers.GameController;
using static Village.Scriptables.ReputationAction.Country;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "ReputationAction", menuName = "Village/Action/ReputationAction")]
	public class ReputationAction : ResourceAction
	{
		public enum Country { CountryA, CountryB }

		public Country country;

		public override void Execute(Villager target)
		{
			float multiplier = GetMultiplier(target);
			if (IsCostCorrect())
			{
				ApplyCosts();
				ApplyEffects(multiplier);
			}
		}

		public override float GetMultiplier(Villager villager)
		{
			if (villager is null) return 1;

			int stat1Val = 0, stat2Val = 0;
			if (stat1) stat1Val = villager.GetEffectiveStatValue(stat1);
			if (stat2) stat2Val = villager.GetEffectiveStatValue(stat2);
			int reputation = GetReputation(villager);
			return 1 + (stat1Val + stat2Val + reputation) * STAT_MULTIPIER;
		}

		public int GetReputation(Villager villager)
		{
			switch (country)
			{
				case CountryA:
					return villager.villagerBase.CountryAReputation;

				case CountryB:
					return villager.villagerBase.CountryBReputation;

				default: throw new System.Exception();
			}
		}
}

}