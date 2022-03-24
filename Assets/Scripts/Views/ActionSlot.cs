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
		private GameObject lockIcon;

		[SerializeField]
		private Transform villagerParent;

		[SerializeField]
		private bool locked;

		public bool Locked { get => locked; private set => locked = value; }

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
			if (eventData.button != PointerEventData.InputButton.Left) return;

			VillagerView dropped = eventData.pointerDrag.GetComponent<VillagerView>();
			if (dropped)
			{
				VillagerView.draggedVillager = null;
				PutVillager(dropped);
				var sound = AudioController.instance.actionPutSound;
				AudioController.instance.PlaySound(sound);
				LoadTooltipData();
			}
		}

		public void PutVillager(VillagerView dropped)
		{
			dropped.transform.SetParent(villagerParent);
			dropped.transform.localPosition = Vector2.zero;
		}

		public void RemoveVillager(bool playSound)
		{
			VillagerView?.MoveToPanel(playSound);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				RemoveVillager(playSound: true);
			}
			else if (eventData.button == PointerEventData.InputButton.Right)
			{
				// TODO: Slot locking
				locked = !locked;
				lockIcon.SetActive(locked);
				var sound = AudioController.instance.slotLock;
				AudioController.instance.PlaySound(sound);
			}
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
