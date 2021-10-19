using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
	public string actionName;
	public Sprite icon;
	public VillagerStat stat1, stat2;

	public List<EffectAmount> effects;

	public abstract void Apply(Villager target);
}
