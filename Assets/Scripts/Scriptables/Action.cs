using System.Collections.Generic;
using UnityEngine;
using static Village.Controllers.GameController;
using static Village.Scriptables.Effect;
using static Village.Scriptables.Resource;
using UnityEngine.Serialization;
using Lean.Localization;
using System.Collections;

namespace Village.Scriptables
{
	public abstract class Action : ScriptableObject, IAction
	{
		[SerializeField]
		private string localeActionName;

		[SerializeField]
		private string localeDescription;

		public Sprite icon;
		public VillagerStat stat1, stat2;
		public int executionPriority;
		public List<ResourceAmount> costs;
		public List<EffectAmount> effects;

		#region Interface properties
		public Sprite Icon => icon;
		public VillagerStat Stat1 => stat1;
		public VillagerStat Stat2 => stat2;
		public List<ResourceAmount> Costs => costs;
		public List<EffectAmount> Effects => effects;
		public string ActionName => LeanLocalization.GetTranslationText(localeActionName);
		public string Description => LeanLocalization.GetTranslationText(localeDescription);
		public int ExecutionPriority => executionPriority;
		#endregion

		public abstract IEnumerator Execute(Villager target);

		protected void ApplyCosts()
		{
			foreach(var cost in Costs)
			{
				instance.AddRemoveResource(cost.resource, -cost.Amount);
			}
		}

		protected bool IsCostCorrect()
		{
			foreach(var cost in Costs)
			{
				int amount = instance.GetResourceAmount(cost.resource);
				if (cost.Amount > amount)
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
		public abstract float GetMultiplier(Villager villager);
	}
}