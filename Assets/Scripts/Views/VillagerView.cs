using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Village.Controllers;
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

		public Villager Villager => _villager;

		public Transform PrevParent { get; private set; }

		public int PrevSiblingIndex { get; private set; }

		public int SortIndex;

		public VillagerView PlaceholderClone { get; private set; }

		public void Load(Villager villager, VillagerController controller)
		{
			this._villager = villager;
			this.controller = controller;
			this.dragParent = controller.dragParent;

			icon.sprite = villager.villagerBase.avatar;
			name = villager.villagerBase.villagerName;
			SortIndex = transform.GetSiblingIndex();
			Refresh();
		}

		public void SetHealtBarVisibility(bool visibilty)
		{
			healthBar.gameObject.SetActive(visibilty);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left) return;

			PrevParent = transform.parent;
			PrevSiblingIndex = transform.GetSiblingIndex();
			draggedVillager = this;
			var slot = GetComponentInParent<ActionSlot>();
			var rt = GetComponent<RectTransform>();

			if (!slot)
			{
				PlaceholderClone = Instantiate(this, transform.parent);
				PlaceholderClone.transform.SetSiblingIndex(PrevSiblingIndex);
			}

			rt.sizeDelta = Vector2.one * dragSpriteSize;
			rt.SetParent(dragParent);
			controller.RefreshGUI();

			GetComponent<Image>().raycastTarget = false;
			blockTooltip = true;
			PlayMoveSound();
		}

		private static void PlayMoveSound()
		{
			var sound = AudioController.instance.villagerMoveSound;
			AudioController.instance.PlaySound(sound);
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left) return;

			transform.position = ClampedMousePos(Input.mousePosition);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left) return;

			var slot = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<ActionSlot>();
			var villager = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<Villager>();

			if (!slot && !villager) // if wrong placement
			{
				MoveToPanel(playSound: false);
			}

			if (PlaceholderClone)
			{
				Destroy(PlaceholderClone.gameObject);
			}

			GetComponent<Image>().raycastTarget = true;
			draggedVillager = null;
		}

		public void OnDrop(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left) return;

			VillagerView dropped = eventData.pointerDrag.GetComponent<VillagerView>();
			draggedVillager = null;

			var parent = transform.parent;
			var size = transform.GetComponent<RectTransform>().sizeDelta;
			var index = transform.GetSiblingIndex();

			var droppedSize = dropped.GetComponent<RectTransform>().sizeDelta;
			int droppedIndex = index < dropped.PrevSiblingIndex ? index : index + 1;

			this.transform.SetParent(dropped.PrevParent);
			this.transform.localPosition = Vector2.zero;
			this.transform.GetComponent<RectTransform>().sizeDelta = droppedSize;
			this.transform.SetSiblingIndex(dropped.PrevSiblingIndex);

			dropped.transform.SetParent(parent);
			dropped.transform.localPosition = Vector2.zero;
			dropped.transform.GetComponent<RectTransform>().sizeDelta = size;
			dropped.transform.SetSiblingIndex(droppedIndex);

			var temp = dropped.SortIndex;
			dropped.SortIndex = SortIndex;
			SortIndex = temp;

			var droppedSlot = dropped.GetComponentInParent<ActionSlot>();

			// if dropped to slot refresh tooltip
			if (droppedSlot)
			{
				ActionTooltip.instance.Load(droppedSlot);
			}
			dropped.blockTooltip = droppedSlot;
			this.blockTooltip = GetComponentInParent<ActionSlot>();

			controller.SortVillagers();
			controller.RefreshGUI();
		}

		public void Refresh()
		{
			healthBar.value = (float)Villager.Health / GameController.MAX_HEALTH;
		}

		public void MoveToPanel(bool playSound)
		{
			GetComponent<Image>().raycastTarget = true;
			blockTooltip = false;
			controller.PutVillager(this);
			if (playSound) PlayMoveSound();
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