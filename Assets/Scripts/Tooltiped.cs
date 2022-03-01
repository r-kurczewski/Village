using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tooltiped : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public float showTime;

	protected GameObject tooltip;

	private float time;
	private bool tooltipShown;
	private bool mouseOver;
	protected bool blockTooltip;

	protected void Start()
	{
		SetTooltipObject();
	}

	private void Update()
	{
		TooltipUpdate();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseOver = true;
	}

	private void TooltipUpdate()
	{
		if (!mouseOver) return;

		if (blockTooltip)
		{
			HideTooltip();
			return;
		} 
		else if (time > showTime)
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

	protected abstract void LoadTooltipData();

	protected abstract void SetTooltipObject();

	private void ShowTooltip()
	{
		tooltip.SetActive(true);
		LoadTooltipData();
		tooltipShown = true;
	}

	protected void HideTooltip()
	{
		tooltip.SetActive(false);
		tooltipShown = false;
	}
}
