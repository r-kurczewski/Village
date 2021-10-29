using System.Collections.Generic;
using UnityEngine;
using Village.Scriptables;
using static Village.Scriptables.Effect;
using static Village.Scriptables.Resource;

public interface IAction
{
	public string ActionName { get; }
	public Sprite Icon { get; }
	public List<ResourceAmount> Costs { get; }
	public List<EffectAmount> Effects { get; }
	public VillagerStat Stat1 { get; }
	public VillagerStat Stat2 { get; }
	public void Execute(Villager target);
}