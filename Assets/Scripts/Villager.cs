using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Villager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public string villagerName;
	public Sprite sprite;
	[Range(0, 3)]
	public int health;
	[Range(-3, 3)]
	public int CountryAReputation, CountryBReputation;
	[Range(0, 5)]
	public int strength, gathering, crafting, diplomacy, intelligence;

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
