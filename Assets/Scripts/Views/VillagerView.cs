using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Village.Controllers;
using UnityEngine.Serialization;
using Village.Views.Tooltips;

namespace Village.Views
{
	[SelectionBase]
	public class VillagerView : Tooltiped, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
	{
		public const int dragSpriteSize = 30;

		public static VillagerView draggedVillager;

		[SerializeField]
		private Villager _villager;

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

		public Villager Villager => _villager;

		public Transform PrevParent { get; private set; }

		public int? PrevSiblingIndex { get; private set; }

		public VillagerView PlaceholderClone { get; private set; }

		public void Load(Villager villager, VillagerController controller)
		{
			this._villager = villager;
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
			PrevParent = transform.parent;
			PrevSiblingIndex = transform.GetSiblingIndex();
			draggedVillager = this;
			var slot = GetComponentInParent<ActionSlot>();
			var rt = GetComponent<RectTransform>();

			if (!slot)
			{
				PlaceholderClone = Instantiate(this, transform.parent);
				PlaceholderClone.transform.SetSiblingIndex(PrevSiblingIndex.Value);
			}

			rt.sizeDelta = Vector2.one * dragSpriteSize;
			rt.SetParent(dragParent);
			controller.RefreshGUI();

			GetComponent<Image>().raycastTarget = false;
			blockTooltip = true;
			AudioController.instance.PlaySound(takeVillagerSound);
		}

		public void OnDrag(PointerEventData eventData)
		{
			transform.position = ClampedMousePos(Input.mousePosition);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			var slot = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<ActionSlot>();
			var villager = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<Villager>();

			if (!slot && !villager)
			{
				MoveToPanel(playSound: true);
			}
			if(PlaceholderClone) Destroy(PlaceholderClone.gameObject);
			GetComponent<Image>().raycastTarget = true;
		}

		//public void OnDrop(PointerEventData eventData)
		//{
		//	VillagerView dropped = eventData.pointerDrag.GetComponent<VillagerView>();
		//	ActionSlot slot = transform.parent.GetComponent<ActionSlot>();

		//	//if dropped on villager in a slot
		//	if (slot)
		//	{
		//		Debug.Log($"{dropped.Villager.name} on {Villager.name}");
		//		transform.SetParent(dropped.PrevParent);
		//		transform.localPosition = Vector2.zero;
		//		slot.PutVillager(dropped);

		//		// if dropped villager was in villagers panel
		//		if (dropped.PrevParent == controller.transform)
		//		{
		//			transform.SetSiblingIndex(dropped.PrevSiblingIndex);
		//			controller.RefreshGUI();
		//		}
		//	}
		//}

		public void OnDrop(PointerEventData eventData)
		{
			VillagerView dropped = eventData.pointerDrag.GetComponent<VillagerView>();
			var droppedSize = dropped.GetComponent<RectTransform>().sizeDelta;

			var parent = transform.parent;
			var size = transform.GetComponent<RectTransform>().sizeDelta;
			var index = transform.GetSiblingIndex();

			Debug.Log($"{dropped.Villager.name} on {Villager.name}");

			transform.SetParent(dropped.PrevParent);
			transform.localPosition = Vector2.zero;
			transform.GetComponent<RectTransform>().sizeDelta = droppedSize;
			transform.SetSiblingIndex(dropped.PrevSiblingIndex.Value);
			
			dropped.transform.SetParent(parent);
			dropped.transform.localPosition = Vector2.zero;
			dropped.transform.GetComponent<RectTransform>().sizeDelta = size;
			dropped.transform.SetSiblingIndex(index < dropped.PrevSiblingIndex ? index : index + 1);

			controller.RefreshGUI();
		}

		public void SetHealth(int value)
		{
			healthBar.value = value;
		}

		public void MoveToPanel(bool playSound)
		{
			GetComponent<Image>().raycastTarget = true;
			blockTooltip = false;
			controller.PutVillager(this);
			if (playSound) AudioController.instance.PlaySound(takeVillagerSound);
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
			VillagerTooltip.instance.Load(_villager);
		}

		protected override void SetTooltipObject()
		{
			tooltip = VillagerTooltip.instance.gameObject;
		}
	}
}