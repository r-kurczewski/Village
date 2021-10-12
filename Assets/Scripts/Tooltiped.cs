using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tooltiped : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public float showTime;

	protected GameObject tooltip;

	private float time;
	private bool tooltipShown;
	private bool mouseOver;

	private void Start()
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

	protected abstract void LoadTooltipData();

	protected abstract void SetTooltipObject();

	private void ShowTooltip()
	{
		tooltip.SetActive(true);
		LoadTooltipData();
		tooltipShown = true;
	}

	private void HideTooltip()
	{
		tooltip.SetActive(false);
		tooltipShown = false;
	}
}
