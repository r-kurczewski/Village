using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class ResourceView : Tooltiped
{
	[SerializeField]
	private TMP_Text label;

	[SerializeField]
	private Image icon;

	[SerializeField]
	private Resource resource;

	public Resource Resource => resource;

	public void SetResource(Resource res)
	{
		resource = res;
		icon.sprite = res.icon;
		icon.color = res.color;
	}

	public void SetAmount(int value)
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
