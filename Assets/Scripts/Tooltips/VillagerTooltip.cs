using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class VillagerTooltip : Tooltip<Villager>
{
	public Image icon;
	public TMP_Text villagerName;
	public ReputationBar countryAReputation, countryBReputation;
	public StatBar strengthBar;
	public StatBar gatheringBar;
	public StatBar craftingBar;
	public StatBar diplomacyBar;
	public StatBar intelligenceBar;

	public override void Load(Villager data)
	{
		icon.sprite = data.villagerBase.icon;
		villagerName.text = data.villagerBase.villagerName;
		countryAReputation.SetReputation(data.villagerBase.CountryAReputation);
		countryBReputation.SetReputation(data.villagerBase.CountryBReputation);
		strengthBar.SetStat(data.strength);
		gatheringBar.SetStat(data.gathering);
		craftingBar.SetStat(data.crafting);
		diplomacyBar.SetStat(data.diplomacy);
		intelligenceBar.SetStat(data.intelligence);

	}
}
