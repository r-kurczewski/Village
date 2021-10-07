using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase]
public class Action : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private GameObject tooltip;

	public float showTime;
	private float time;
	private bool tooltipShown;
	private bool mouseOver;

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseOver = true;
	}

	private void Update()
	{
		TooltipUpdate();
	}

	private void TooltipUpdate()
	{
		if (!mouseOver) return;

		if (time > showTime)
		{
			if (!tooltipShown) ShowTooltip();
			tooltip.transform.position = Input.mousePosition;
		}
		time += Time.deltaTime;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseOver = false;
		time = 0;
		HideTooltip();
	}

	private void ShowTooltip()
	{
		tooltip.SetActive(true);
		tooltipShown = true;
	}

	private void HideTooltip()
	{
		tooltip.SetActive(false);
		tooltipShown = false;
	}
}
