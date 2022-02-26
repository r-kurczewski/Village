using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Village.Views.Tooltips
{
	[SelectionBase]
	public class VillagerTooltip : Tooltip<Villager>
	{
		public Image icon;
		public TMP_Text villagerName;
		public ReputationView countryAReputation, countryBReputation;
		public VillageStatView strengthBar;
		public VillageStatView gatheringBar;
		public VillageStatView craftingBar;
		public VillageStatView diplomacyBar;
		public VillageStatView intelligenceBar;

		public override void Load(Villager data)
		{
			icon.sprite = data.villagerBase.avatar;
			villagerName.text = data.villagerBase.villagerName;
			countryAReputation.SetReputation(data.villagerBase.CountryAReputation);
			countryBReputation.SetReputation(data.villagerBase.CountryBReputation);
			strengthBar.SetStat(data.EffectiveStrength, data.Strength);
			gatheringBar.SetStat(data.EffectiveGathering, data.Gathering);
			craftingBar.SetStat(data.EffectiveCrafting, data.Crafting);
			diplomacyBar.SetStat(data.EffectiveDiplomacy, data.Diplomacy);
			intelligenceBar.SetStat(data.EffectiveIntelligence, data.Intelligence);

		}
	}
}