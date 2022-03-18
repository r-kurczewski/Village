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
			VillagerView dropped = eventData.pointerDrag.GetComponent<VillagerView>();
			if (dropped)
			{
				PutVillager(dropped);
				var sound = AudioController.instance.actionPutSound;
				AudioController.instance.PlaySound(sound);
			}
		}

		public void PutVillager(VillagerView dropped)
		{
			dropped.transform.SetParent(transform);
			dropped.transform.localPosition = Vector2.zero;
		}

		public void RemoveVillager()
		{
			VillagerView?.MoveToPanel(playSound: false);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (VillagerView) VillagerView.MoveToPanel(playSound: true);
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
