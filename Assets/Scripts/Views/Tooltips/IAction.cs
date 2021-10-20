using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
	public string ActionName { get; }
	public Sprite Icon { get; }
	public List<ResourceAmount> Costs { get; }
	public List<EffectAmount> Effects { get; }
	public VillagerStat Stat1 { get; }
	public VillagerStat Stat2 { get; }
	public void Apply(Villager target);
}