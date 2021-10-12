using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
	public string actionName;
	public VillagerStat stat1, stat2;

	public abstract void Apply(Villager target);
}
