using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Village.Scriptables;
using static Village.Scriptables.Effect;
using static Village.Scriptables.Resource;

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
		private Color defaultColor;

		[SerializeField]
		private Transform effectsParent;

		[SerializeField]
		private EffectView actionResultPrefab;

		public override void Load(ActionSlot slot)
		{
			if (slot.Action == null)
			{
				Debug.LogWarning("Action is not set!");
				return;
			}

			Clear();

			actionName.text = slot.Action.ActionName;

			bool hasDescription = slot.Action.Description != string.Empty;
			if (hasDescription)
			{
				actionDescription.text = slot.Action.Description;
			}
			actionDescription.transform.parent.gameObject.SetActive(hasDescription);

			if (slot.Action.Stat1)
			{
				statIcon1.sprite = slot.Action.Stat1.statIcon;
				statIcon1.color = slot.Action.Stat1.statColor;
			}
			else
			{
				statIcon1.sprite = defaultIcon;
				statIcon1.color = defaultColor;
			}

			if (slot.Action.Stat2)
			{
				statIcon2.gameObject.SetActive(true);
				statIcon2.sprite = slot.Action.Stat2.statIcon;
				statIcon2.color = slot.Action.Stat2.statColor;
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
			actionResult.SetAmount(Mathf.RoundToInt(eff.value * multiplier));
		}

		private void LoadCost(ResourceAmount res)
		{
			var actionResult = Instantiate(actionResultPrefab, effectsParent);
			actionResult.SetIcon(res.resource.icon);
			actionResult.SetIconColor(res.resource.color);
			actionResult.SetAmount(-res.Amount);
		}

		

		private void RefreshLayout()
		{
			GetComponent<LayoutRefresher>().RefreshContentFitters();
			GetComponent<LayoutRefresher>().RefreshContentFitters();
		}
	}
}
