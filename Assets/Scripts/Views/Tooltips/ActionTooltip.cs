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
using System;

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

			// load tooltip with either slot or dragged villager
			Villager villager = null;
			if (slot.Villager && !VillagerView.draggedVillager?.Villager)
			{
				villager = slot.Villager;
			}
			else if(!slot.Villager && VillagerView.draggedVillager?.Villager)
			{
				villager = VillagerView.draggedVillager?.Villager;
			}

			var stat1 = slot.Action.Stat1;
			var stat2 = slot.Action.Stat2;

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

			int costCount = slot.Action.Costs.Count;
			int effectCount = slot.Action.Effects.Count;

			// loading costs to existing views
			for (int i = 0; i < costCount; i++)
			{
				var cost = slot.Action.Costs[i];
				EffectView view = null;
				if (effectsParent.childCount > i)
				{
					view = effectsParent.GetChild(i).GetComponent<EffectView>();
					view.gameObject.SetActive(true);
				}
				LoadCost(cost, view);
			}

			// loading effects to existing views
			for (int i = 0; i < effectCount; i++)
			{
				var eff = slot.Action.Effects[i];
				float multiplier = slot.Action.GetMultiplier(villager);
				EffectView view = null;
				if (effectsParent.childCount > i + costCount)
				{
					view = effectsParent.GetChild(i + costCount).GetComponent<EffectView>();
					view.gameObject.SetActive(true);
				}
				LoadEffect(eff, multiplier, view);
			}

			// hide other views
			for (int i = costCount + effectCount; i < effectsParent.childCount; i++)
			{
				effectsParent.GetChild(i).gameObject.SetActive(false);
			}

			if (villager)
			{
				villagerOverview.gameObject.SetActive(true);
				villagerAvatar.sprite = villager.Avatar;
				villagerStrengthLabel.text = villager.EffectiveStrength.ToString();
				villagerGatheringLabel.text = villager.EffectiveGathering.ToString();
				villagerCraftingLabel.text = villager.EffectiveCrafting.ToString();
				villagerDiplomacyLabel.text = villager.EffectiveDiplomacy.ToString();
				villagerIntelligenceLabel.text = villager.EffectiveIntelligence.ToString();

				villagerStrengthLabel.color = GetStatColor(villager.StrengthReference, stat1, stat2);
				villagerGatheringLabel.color = GetStatColor(villager.GatheringReference, stat1, stat2);
				villagerCraftingLabel.color = GetStatColor(villager.CraftingReference, stat1, stat2);
				villagerDiplomacyLabel.color = GetStatColor(villager.DiplomacyReference, stat1, stat2);
				villagerIntelligenceLabel.color = GetStatColor(villager.IntelligenceReference, stat1, stat2);
			}
			else villagerOverview.gameObject.SetActive(false);

			RefreshLayout();
		}

		private void LoadCost(ResourceAmount res, EffectView view = null)
		{
			if (view is null) view = Instantiate(actionResultPrefab, effectsParent);

			view.SetIcon(res.resource.icon);
			view.SetIconColor(res.resource.color);
			view.SetAmount(-res.Amount);
			if (GameController.instance.GetResourceAmount(res.resource) < res.Amount)
			{
				view.SetFontColor(negativeValue);
			}
			else
			{
				view.SetFontColor(defaultValue);
			}
		}

		private void LoadEffect(EffectAmount eff, float multiplier, EffectView view = null)
		{
			if (view is null) view = Instantiate(actionResultPrefab, effectsParent);

			int effectiveAmount = Mathf.RoundToInt(eff.value * multiplier);

			view.SetIcon(eff.effect.icon);
			view.SetIconColor(eff.effect.color);
			view.SetAmount(effectiveAmount);
			if (effectiveAmount > eff.value)
			{
				view.SetFontColor(effectiveValue);
			}
			else if (effectiveAmount < eff.value)
			{
				view.SetFontColor(negativeValue);
			}
			else
			{
				view.SetFontColor(defaultValue);
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
