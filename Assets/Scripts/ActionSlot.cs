using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[SelectionBase]
public class ActionSlot : Tooltiped
{
	[SerializeField]
	private IAction action;

	[SerializeField]
	private Image icon;

	public void Load(IAction action)
	{
		this.action = action;
		icon.sprite = action.Icon;
	}

	protected override void LoadTooltipData()
	{
		ActionTooltip.instance.Load(action);
	}

	protected override void SetTooltipObject()
	{
		tooltip = ActionTooltip.instance.gameObject;
	}
}
