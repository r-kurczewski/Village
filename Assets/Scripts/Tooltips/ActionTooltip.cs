using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class ActionTooltip : Tooltip<Action>
{
	[SerializeField]
	private TMP_Text actionName;

	[SerializeField]
	private Image statIcon1, statIcon2;

	[SerializeField]
	private Transform effectsParent;

	[SerializeField]
	private EffectView actionResultPrefab;

	public override void Load(Action action)
	{
		if (action == null) return;

		actionName.text = action.actionName;

		if (action.stat1)
		{
			statIcon1.gameObject.SetActive(true);
			statIcon1.sprite = action.stat1.statIcon;
			statIcon1.color = action.stat1.statColor;
		}
		else statIcon1.gameObject.SetActive(false);

		if (action.stat2)
		{
			statIcon2.gameObject.SetActive(true);
			statIcon2.sprite = action.stat2.statIcon;
			statIcon2.color = action.stat2.statColor;
		}
		else statIcon2.gameObject.SetActive(false);

		foreach(Transform item in effectsParent)
		{
			Destroy(item.gameObject);
		}

		foreach (var eff in action.effects)
		{
			var actionResult = Instantiate(actionResultPrefab, effectsParent);
			actionResult.SetIcon(eff.effect.icon);
			actionResult.SetIconColor(eff.effect.color);
			actionResult.SetAmount(eff.amount);
		}

		RefreshLayout();

	}

	private void RefreshLayout()
	{
		GetComponent<LayoutRefresher>().RefreshContentFitters();
		GetComponent<LayoutRefresher>().RefreshContentFitters();
	}
}
