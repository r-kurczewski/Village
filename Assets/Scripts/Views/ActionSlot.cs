using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Village.Controllers;
using Village.Scriptables;
using Village.Views.Tooltips;

namespace Village.Views
{
	[SelectionBase]
	public class ActionSlot : Tooltiped, IDropHandler, IPointerClickHandler
	{
		[SerializeField]
		private IAction action;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private AudioClip putVillagerSound;

		public IAction Action => action;

		public VillagerView VillagerView => GetComponentInChildren<VillagerView>();

		public Villager Villager => GetComponentInChildren<Villager>();

		public void Load(IAction action)
		{
			this.action = action;
			icon.sprite = action.Icon;
			name = action.ActionName;
		}

		public void OnDrop(PointerEventData eventData)
		{
			VillagerView villager = eventData.pointerDrag.GetComponent<VillagerView>();
			if (villager)
			{
				villager.transform.SetParent(transform);
				villager.transform.localPosition = Vector2.zero;
				AudioController.instance.PlaySound(putVillagerSound);
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (VillagerView) VillagerView.MoveToPanel();
		}

		protected override void LoadTooltipData()
		{
			ActionTooltip.instance.Load(this);
		}

		protected override void SetTooltipObject()
		{
			tooltip = ActionTooltip.instance.gameObject;
		}

		public float GetActionMultiplier()
		{
			return action.GetMultiplier(Villager);
		}
	}
}
