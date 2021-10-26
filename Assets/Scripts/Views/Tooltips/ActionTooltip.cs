using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Village.Views.Tooltips
{
	[SelectionBase]
	public class ActionTooltip : Tooltip<IAction>
	{
		[SerializeField]
		private TMP_Text actionName;

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

		public override void Load(IAction action)
		{
			if (action == null)
			{
				Debug.LogWarning("Action is not set!");
				return;
			}

			Clear();

			actionName.text = action.ActionName;

			if (action.Stat1)
			{
				statIcon1.sprite = action.Stat1.statIcon;
				statIcon1.color = action.Stat1.statColor;
			}
			else
			{
				statIcon1.sprite = defaultIcon;
				statIcon1.color = defaultColor;
			}

			if (action.Stat2)
			{
				statIcon2.gameObject.SetActive(true);
				statIcon2.sprite = action.Stat2.statIcon;
				statIcon2.color = action.Stat2.statColor;
			}
			else statIcon2.gameObject.SetActive(false);

			foreach (var cost in action.Costs)
			{
				LoadCost(cost);
			}

			foreach (var eff in action.Effects)
			{
				LoadEffect(eff);
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

		private void LoadEffect(EffectAmount eff)
		{
			var actionResult = Instantiate(actionResultPrefab, effectsParent);
			actionResult.SetIcon(eff.effect.icon);
			actionResult.SetIconColor(eff.effect.color);
			actionResult.SetAmount(eff.amount);
		}

		private void LoadCost(ResourceAmount res)
		{
			var actionResult = Instantiate(actionResultPrefab, effectsParent);
			actionResult.SetIcon(res.resource.icon);
			actionResult.SetIconColor(res.resource.color);
			actionResult.SetAmount(-res.amount);
		}

		private void RefreshLayout()
		{
			GetComponent<LayoutRefresher>().RefreshContentFitters();
			GetComponent<LayoutRefresher>().RefreshContentFitters();
		}
	}
}
