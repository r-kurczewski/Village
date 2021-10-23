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
		resourcePanel.Refresh();
	}

	public  void AddRemoveVillagerHealth(int value)
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