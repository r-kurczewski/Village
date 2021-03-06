using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;
using Village.Views;
using static Village.Scriptables.Effect;
using static Village.Scriptables.Resource;
using static Village.Controllers.GameController;

namespace Village.Scriptables
{
	public class BuildAction : IAction
	{
		[SerializeField]
		private Action buildBaseAction;
		private LocationView buildingView;
		private MapBuilding buildingBase;

		public BuildAction(Action buildBaseAction, MapBuilding buildingBase, LocationView buildingView)
		{
			this.buildBaseAction = buildBaseAction;
			this.buildingView = buildingView;
			this.buildingBase = buildingBase;
		}

		public string ActionName => LeanLocalization.GetTranslationText(buildingBase.LocaleBuildActionName);
		public string Description => LeanLocalization.GetTranslationText(buildingBase.localeBuildingDescription);
		public Sprite Icon => buildBaseAction.icon;
		public int ExecutionPriority => buildBaseAction.ExecutionPriority;
		public List<EffectAmount> Effects => new List<EffectAmount>();
		public List<ResourceAmount> Costs => buildingBase.buildingCost;
		public VillagerStat Stat1 => buildBaseAction.stat1;
		public VillagerStat Stat2 => buildBaseAction.stat2;

		public IEnumerator Execute(Villager target)
		{
			if (buildingView.Location is MapBuilding)
			{
				if (IsCostCorrect())
				{
					ApplyCosts();
					buildingView.Build();
					instance.AddLogSubEntry(new LogController.LogSubEntry(buildingBase.localeLogBuilt));
				}
			}
			else Debug.LogWarning("Trying to build not-building location!");
			yield return null;
		}

		protected void ApplyCosts()
		{
			foreach (var cost in Costs)
			{
				instance.AddRemoveResource(cost.resource, -cost.Amount);
			}
		}

		protected bool IsCostCorrect()
		{
			foreach (var cost in Costs)
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
			foreach (var eff in Effects)
			{
				int finalValue = Mathf.RoundToInt(eff.value * villagerMultiplier);
				eff.effect.Apply(finalValue);
			}
		}

		public float GetMultiplier(Villager target)
		{
			return 1;
		}
	}
}
