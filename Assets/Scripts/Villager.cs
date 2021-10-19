using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase]
public class Villager : Tooltiped
{
	public VillagerBase villagerBase;
	public int strength, gathering, crafting, diplomacy, intelligence;
	
	[Range(0, 4)]
	public int health;

	public new void Start()
	{
		SetTooltipObject();
		if (villagerBase) Load(villagerBase);
	}

	public void Load(VillagerBase villager)
	{
		villagerBase = villager;
		strength = villager.baseStrength;
		gathering = villager.baseGathering;
		crafting = villager.baseCrafting;
		diplomacy = villager.baseDiplomacy;
		intelligence = villager.baseIntelligence;
	}

	protected override void LoadTooltipData()
	{
		VillagerTooltip.instance.Load(this);
	}

	protected override void SetTooltipObject()
	{
		tooltip = VillagerTooltip.instance.gameObject;
	}
}
