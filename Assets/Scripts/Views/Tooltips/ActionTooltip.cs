using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Village.Controllers;
using Village.Scriptables;
using static Village.Scriptables.Effect;
using static Village.Scriptables.Resource;
using UnityEngine.Serialization;

namespace Village.Views.Tooltips
{
	[SelectionBase]
	public class ActionTooltip : Tooltip<ActionSlot>
	{
		[SerializeField]
		private TMP_Text actionName;

		[SerializeField]
		private TMP_Text actionDescription;

		[SerializeField]
		private Image statIcon1, statIcon2;

		[SerializeField]
		private Sprite defaultIcon;

		[SerializeField]
		private Transform effectsParent;

		[SerializeField]
		private Transform villagerOverview;

		[SerializeField]
		private Image villagerAvatar;

		[SerializeField]
		private TMP_Text villagerStrengthLabel;

		[SerializeField]
		private TMP_Text villagerGatheringLabel;

		[SerializeField]
		private TMP_Text villagerCraftingLabel;

		[SerializeField]
		private TMP_Text villagerDiplomacyLabel;

		[SerializeField]
		private TMP_Text villagerIntelligenceLabel;

		[SerializeField]
		private EffectView actionResultPrefab;

		[SerializeField]
		private Color defaultValue;

		[SerializeField]
		private Color effectiveValue;

		[SerializeField]
		private Color negativeValue;

		public override void Load(ActionSlot slot)
		{
			if (slot.Action == null)
			{
				Debug.LogWarning("Action is not set!");
				return;
			}

			var stat1 = slot.Action.Stat1;
			var stat2 = slot.Action.Stat2;

			Clear();
			actionName.text = slot.Action.ActionName;

			bool hasDescription = slot.Action.Description != null;
			bool hideDescription = GameSettings.SimplifiedTooltips;
			if (hasDescription)
			{
				actionDescription.text = slot.Action.Description;
			}
			actionDescription.transform.parent.gameObject.SetActive(hasDescription && !hideDescription);

			if (stat1)
			{
				statIcon1.sprite = stat1.backgroundIcon;
				statIcon1.color = stat1.color;
			}
			else
			{
				statIcon1.sprite = defaultIcon;
				statIcon1.color = defaultValue;
			}

			if (stat2)
			{
				statIcon2.gameObject.SetActive(true);
				statIcon2.sprite = stat2.backgroundIcon;
				statIcon2.color = stat2.color;
			}
			else statIcon2.gameObject.SetActive(false);

			foreach (var cost in slot.Action.Costs)
			{
				LoadCost(cost);
			}

			foreach (var eff in slot.Action.Effects)
			{
				float multiplier = slot.Action.GetMultiplier(slot.Villager);
				LoadEffect(eff, multiplier);
			}

			if (slot.Villager)
			{
				villagerOverview.gameObject.SetActive(true);
				villagerAvatar.sprite = slot.Villager.Avatar;
				villagerStrengthLabel.text = slot.Villager.EffectiveStrength.ToString();
				villagerGatheringLabel.text = slot.Villager.EffectiveGathering.ToString();
				villagerCraftingLabel.text = slot.Villager.EffectiveCrafting.ToString();
				villagerDiplomacyLabel.text = slot.Villager.EffectiveDiplomacy.ToString();
				villagerIntelligenceLabel.text = slot.Villager.EffectiveIntelligence.ToString();

				villagerStrengthLabel.color = GetStatColor(slot.Villager.StrengthReference, stat1, stat2);
				villagerGatheringLabel.color = GetStatColor(slot.Villager.GatheringReference, stat1, stat2);
				villagerCraftingLabel.color = GetStatColor(slot.Villager.CraftingReference,stat1, stat2);
				villagerDiplomacyLabel.color = GetStatColor(slot.Villager.DiplomacyReference, stat1, stat2);
				villagerIntelligenceLabel.color = GetStatColor(slot.Villager.IntelligenceReference, stat1, stat2);
			}
			else villagerOverview.gameObject.SetActive(false);

			RefreshLayout();
		}

		private void Clear()
		{
			foreach (Transform item in effectsParent)
			{
				Destroy(item.gameObject);
			}
		}

		private void LoadEffect(EffectAmount eff, float multiplier)
		{
			var actionResult = Instantiate(actionResultPrefab, effectsParent);
			actionResult.SetIcon(eff.effect.icon);
			actionResult.SetIconColor(eff.effect.color);
			int effectiveAmount = Mathf.RoundToInt(eff.value * multiplier);
			actionResult.SetAmount(effectiveAmount);
			if (effectiveAmount > eff.value)
			{
				actionResult.SetFontColor(effectiveValue);
			}
			else if (effectiveAmount < eff.value)
			{
				actionResult.SetFontColor(negativeValue);
			}
			else
			{
				actionResult.SetFontColor(defaultValue);
			}
		}

		private void LoadCost(ResourceAmount res)
		{
			var actionResult = Instantiate(actionResultPrefab, effectsParent);
			actionResult.SetIcon(res.resource.icon);
			actionResult.SetIconColor(res.resource.color);
			actionResult.SetAmount(-res.Amount);
			if (GameController.instance.GetResourceAmount(res.resource) < res.Amount)
			{
				actionResult.SetFontColor(negativeValue);
			}
			else
			{
				actionResult.SetFontColor(defaultValue);
			}
		}

		private Color GetStatColor(VillagerStat stat, VillagerStat stat1, VillagerStat stat2)
		{
			if (stat.name == stat1?.name || stat.name == stat2?.name)
			{
				return effectiveValue;
			}
			else return defaultValue;
		}

		private void RefreshLayout()
		{
			GetComponent<LayoutRefresher>().RefreshContentFitters();
			GetComponent<LayoutRefresher>().RefreshContentFitters();
		}
	}
}
