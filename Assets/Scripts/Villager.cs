using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase]
public class Villager : Tooltiped
{
	public string villagerName;
	public Sprite sprite;
	[Range(0, 3)]
	public int health;
	[Range(-3, 3)]
	public int CountryAReputation, CountryBReputation;
	[Range(0, 5)]
	public int strength, gathering, crafting, diplomacy, intelligence;

	protected override void LoadTooltipData()
	{
		VillagerTooltip.instance.Load(this);
	}

	protected override void SetTooltipObject()
	{
		tooltip = VillagerTooltip.instance.gameObject;
	}
}
