using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Village.Controllers;
using UnityEngine.Serialization;
using Village.Views.Tooltips;

namespace Village.Views
{
	[SelectionBase]
	public class VillagerView : Tooltiped, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private const int dragSpriteSize = 30;

		public static VillagerView draggedVillager;

		[SerializeField]
		private Villager villager;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private Slider healthBar;

		[SerializeField]
		private Transform dragParent;

		[SerializeField]
		private VillagerController controller;

		[SerializeField]
		private AudioClip takeVillagerSound;

		public Villager Villager => villager;

		public void Load(Villager villager, VillagerController controller)
		{
			this.villager = villager;
			this.controller = controller;
			this.dragParent = controller.dragParent;

			icon.sprite = villager.villagerBase.avatar;
			healthBar.value = villager.Health;
			name = villager.villagerBase.villagerName;
		}

		public void SetHealtBarVisibility(bool visibilty)
		{
			healthBar.gameObject.SetActive(visibilty);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			draggedVillager = this;
			var rt = GetComponent<RectTransform>();
			rt.sizeDelta = Vector2.one * dragSpriteSize;
			rt.SetParent(dragParent);
			GetComponent<Image>().raycastTarget = false;
			blockTooltip = true;
			Debug.Log("Beg");
			AudioController.instance.PlaySound(takeVillagerSound);
		}

		public void OnDrag(PointerEventData eventData)
		{
			transform.position = ClampedMousePos(Input.mousePosition);
			Debug.Log("Mid");
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			Debug.Log("End");
			var slot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<ActionSlot>();
			if (!slot)
			{
				MoveToPanel();
				AudioController.instance.PlaySound(takeVillagerSound);
			}
			GetComponent<Image>().raycastTarget = true;
		}

		public void SetHealth(int value)
		{
			healthBar.value = value;
		}

		public void MoveToPanel()
		{
			GetComponent<Image>().raycastTarget = true;
			blockTooltip = false;
			controller.PutVillager(this);
		}

		private Vector2 ClampedMousePos(Vector2 mousePos)
		{
			var result = mousePos;
			result.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
			result.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
			return result;
		}
		protected override void LoadTooltipData()
		{
			VillagerTooltip.instance.Load(villager);
		}

		protected override void SetTooltipObject()
		{
			tooltip = VillagerTooltip.instance.gameObject;
		}
	}
}