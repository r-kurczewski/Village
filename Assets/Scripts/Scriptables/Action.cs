using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject, IAction
{
	public string actionName;
	public Sprite icon;
	public VillagerStat stat1, stat2;
	public List<ResourceAmount> costs;
	public List<EffectAmount> effects;

	#region Properties
	public string ActionName => actionName;
	public Sprite Icon => icon;
	public VillagerStat Stat1 => stat1;
	public VillagerStat Stat2 => stat2;
	public List<ResourceAmount> Costs => costs;
	public List<EffectAmount> Effects => effects;
	#endregion

	public abstract void Apply(Villager target);
}
