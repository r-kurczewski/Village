using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerPanelView : MonoBehaviour
{
	[SerializeField]
	private VillagerView villagerPrefab;

	public Transform dragParent;

	public void PutVillager(VillagerView villager)
	{
		villager.transform.SetParent(transform);
	}

	public Villager CreateNewVillager(VillagerBase villagerBase)
	{
		VillagerView view = Instantiate(villagerPrefab, transform);
		Villager villager = view.GetComponent<Villager>();
		villager.Load(villagerBase);
		view.Load(villager, this);
		return villager;
	}
}
