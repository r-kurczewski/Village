using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[SelectionBase]
public class ActionSlot : Tooltiped, IDropHandler, IPointerClickHandler
{
	[SerializeField]
	private IAction action;

	[SerializeField]
	private Image icon;

	public VillagerView VillagerView
	{
		get
		{
			return GetComponentInChildren<VillagerView>();
		}
	}

	public Villager Villager
	{
		get
		{
			return GetComponentInChildren<Villager>();
		}
	}

	public void Load(IAction action)
	{
		this.action = action;
		icon.sprite = action.Icon;
	}

	public void OnDrop(PointerEventData eventData)
	{
		VillagerView villager = eventData.pointerDrag.GetComponent<VillagerView>();
		if (villager)
		{
			villager.transform.SetParent(transform);
			villager.transform.localPosition = Vector2.zero;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(VillagerView) VillagerView.MoveToVillagerPanel();
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
