using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController instance;

	[SerializeField]
	private ResourcePanelView resourcePanel;

	[SerializeField]
	private VillagerPanelView villagerPanel;

	[SerializeField]
	private List<VillagerBase> startVillagers;

	[SerializeField]
	private GameData data;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else Debug.LogWarning("There is more than one GameController in the scene!");
	}
	private void Start()
	{
		var randomVillagers = startVillagers.OrderBy(x => UnityEngine.Random.value).Take(6);
		foreach (var villagerBase in randomVillagers)
		{
			data.villagers.Add(villagerPanel.CreateNewVillager(villagerBase));
		}
		resourcePanel.Refresh();
	}

	public void AddRemoveVillagerHealth(int value)
	{
		data.villagers.ForEach(x => x.health += value);
	}
	public void AddRemoveResource(Resource resource, int amount)
	{
		data.resources.First(x => x.resource == resource).amount += amount;
		resourcePanel.Refresh();
	}

	public int GetResourceAmount(Resource resource)
	{
		return data.resources.First(x => x.resource == resource).amount;
	}

	public void SetPredictionFactor(int value)
	{
		data.predictionFactor = value;
	}
}