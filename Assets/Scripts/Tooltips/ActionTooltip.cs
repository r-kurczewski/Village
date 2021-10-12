using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class ActionTooltip : Tooltip<Action>
{
	public TMP_Text actionName;
	public Image statIcon1, statIcon2;


	public override void Load(Action data)
	{
		if (data == null) return;
		actionName.text = data.actionName;
		statIcon1.sprite = data.stat1.statIcon;
		statIcon1.color = data.stat1.statColor;
		if (data.stat2 != null)
		{
			statIcon2.gameObject.SetActive(true);
			statIcon2.sprite = data.stat2.statIcon;
			statIcon2.color = data.stat2.statColor;
		}
		else
		{
			statIcon2.gameObject.SetActive(false);
		}
		RefreshLayout();

	}

	private void RefreshLayout()
	{
		GetComponent<LayoutRefresher>().RefreshContentFitters();
		GetComponent<LayoutRefresher>().RefreshContentFitters();
	}
}
