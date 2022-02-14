using System.Collections.Generic;
using UnityEngine;
using Village.Scriptables;
using Village.Views;
using static Village.Controllers.GameController;
using static Village.Scriptables.Effect;
using static Village.Scriptables.Resource;

public class BuildAction : IAction
{
	public Action buildBaseAction;
	private LocationView buildingView;
	private MapBuilding buildingBase;

	public BuildAction(Action buildBaseAction, MapBuilding buildingBase, LocationView buildingView)
	{
		this.buildBaseAction = buildBaseAction;
		this.buildingView = buildingView;
		this.buildingBase = buildingBase;
	}

	public string ActionName => buildingBase.buildActionName;
	public string Description => buildingBase.buildingDescription;
	public Sprite Icon => buildBaseAction.icon;
	public List<EffectAmount> Effects => new List<EffectAmount>();
	public List<ResourceAmount> Costs => buildingBase.buildingCost;
	public VillagerStat Stat1 => buildBaseAction.stat1;
	public VillagerStat Stat2 => buildBaseAction.stat2;

	public void Execute(Villager target)
	{
		if (IsCostCorrect())
		{
			ApplyCosts();
			if (buildingView.Location is MapBuilding building)
			{
				building.ApplyOnetimeBonus();
				buildingView.SetAsBuilt();
			}
			else Debug.LogWarning("Trying to build not-building location!");
		}
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
