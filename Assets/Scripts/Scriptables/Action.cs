using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;
using static Village.Scriptables.Effect;
using static Village.Scriptables.Resource;
using static Village.Controllers.GameController;

namespace Village.Scriptables
{
	public abstract class Action : ScriptableObject, IAction
	{
		public string actionName;
		public Sprite icon;
		public VillagerStat stat1, stat2;
		public List<ResourceAmount> costs;
		public List<EffectAmount> effects;

		#region Interface properties
		public string ActionName => actionName;
		public Sprite Icon => icon;
		public VillagerStat Stat1 => stat1;
		public VillagerStat Stat2 => stat2;
		public List<ResourceAmount> Costs => costs;
		public List<EffectAmount> Effects => effects;
		#endregion

		public abstract void Execute(Villager target);

		protected void ApplyCosts()
		{
			foreach(var cost in Costs)
			{
				instance.AddRemoveResource(cost.resource, -cost.amount);
			}
		}

		protected bool IsCostCorrect()
		{
			foreach(var cost in Costs)
			{
				int amount = instance.GetResourceAmount(cost.resource);
				if (cost.amount > amount)
				{
					return false;
				}
			}
			return true;
		}

		protected void ApplyEffects(float villagerMultiplier)
		{
			foreach(var eff in effects)
			{
				int finalValue = Mathf.RoundToInt(eff.value * villagerMultiplier);
				eff.effect.Apply(finalValue);
			}
		}

		protected float GetVillagerMultiplier(Villager villager)
		{
			int stat1Val = 0, stat2Val = 0;
			if (stat1) stat1Val = villager.GetStatValue(stat1);
			if (stat2) stat2Val = villager.GetStatValue(stat2);
			return 1 + (stat1Val + stat2Val) * STAT_MULTIPIER;
		}
	}
}