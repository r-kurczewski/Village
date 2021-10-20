using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

	public string ActionName => buildingBase.buildDescription;

	public Sprite Icon => buildBaseAction.icon;

	public List<EffectAmount> Effects => new List<EffectAmount>();
	public List<ResourceAmount> Costs => buildingBase.buildingCost;
	public VillagerStat Stat1 => buildBaseAction.stat1;
	public VillagerStat Stat2 => buildBaseAction.stat2;

	public void Apply(Villager target)
	{
		buildingView.Build();
	}
}
