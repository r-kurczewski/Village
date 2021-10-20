using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VillagerBase", menuName = "Village/VillagerBase")]
public class VillagerBase : ScriptableObject
{
	public string villagerName;
	public Sprite icon;

	[Range(-3, 3)]
	public int CountryAReputation, CountryBReputation;

	[Range(0, 5)]
	public int
		baseStrength,
		baseGathering,
		baseCrafting,
		baseDiplomacy,
		baseIntelligence;
}
