using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[SelectionBase]
public class ResourceView : Tooltiped
{
	[SerializeField] private TMP_Text label;
	public Resource resource;

	public void SetValue(int value)
	{
		label.text = value.ToString();
	}

	protected override void LoadTooltipData()
	{
		TextTooltip.instance.Load(resource.resourceName);
	}

	protected override void SetTooltipObject()
	{
		tooltip = TextTooltip.instance.gameObject;
	}
}
