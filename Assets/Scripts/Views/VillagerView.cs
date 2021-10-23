using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
	private VillagerPanelView villagerPanel;

	#region dragging data
	private Transform prevParent = default;
	private Vector2 prevPosition = default;
	private Vector2 prevSize = default;
	private int prevChildIndex;
	#endregion

	//public new void Start()
	//{
	//	SetTooltipObject();
	//	if (villager) Load(villager);
	//}

	public void Load(Villager villager, VillagerPanelView villagerPanel)
	{
		this.villager = villager;
		icon.sprite = villager.villagerBase.avatar;
		healthBar.value = villager.health;
		name = villager.villagerBase.villagerName;

		this.villagerPanel = villagerPanel;
		dragParent = villagerPanel.dragParent;
	}

	public void SetHealtBarVisibility(bool visibilty)
	{
		healthBar.gameObject.SetActive(visibilty);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		var rt = GetComponent<RectTransform>();
		prevParent = rt.parent;
		prevPosition = rt.localPosition;
		prevSize = rt.sizeDelta;
		prevChildIndex = rt.GetSiblingIndex();

		GetComponent<RectTransform>().sizeDelta = Vector2.one * dragSpriteSize;
		GetComponent<Image>().raycastTarget = false;
		transform.SetParent(dragParent);
		draggedVillager = this;
		blockTooltip = true;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = ClampedMousePos(Input.mousePosition);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		var slot = eventData.pointerCurrentRaycast.gameObject.GetComponent<ActionSlot>();
		if (!slot) MoveToVillagerPanel();
		GetComponent<Image>().raycastTarget = true;
	}

	//private void RevertDrag()
	//{
	//	var rt = GetComponent<RectTransform>();
	//	rt.SetParent(prevParent);
	//	rt.sizeDelta = prevSize;
	//	rt.localPosition = prevPosition;
	//	rt.SetSiblingIndex(prevChildIndex);
	//	blockTooltip = false;
	//	draggedVillager = null;
	//	GetComponent<Image>().raycastTarget = true;
	//}

	public void MoveToVillagerPanel()
	{
		GetComponent<Image>().raycastTarget = true;
		blockTooltip = false;
		villagerPanel.PutVillager(this);
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
