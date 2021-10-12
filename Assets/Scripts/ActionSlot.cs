using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase]
public class ActionSlot : Tooltiped
{
	public Action action;

	protected override void LoadTooltipData()
	{
		ActionTooltip.instance.Load(action);
	}

	protected override void SetTooltipObject()
	{
		tooltip = ActionTooltip.instance.gameObject;
	}
}
