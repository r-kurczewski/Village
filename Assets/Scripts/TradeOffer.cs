using System;
using UnityEngine;
using Village.Scriptables;
using static Village.Controllers.GameController;
using static TradeOffer.TradeMode;

[Serializable]
public class TradeOffer
{
	public enum TradeMode { Buy, Sell }

	public Resource resource;
	public TradeMode mode;

	public TradeOffer() { }

	public TradeOffer(Resource resource, TradeMode mode)
	{
		this.resource = resource;
		this.mode = mode;
	}

	public int GetCost(Villager villager)
	{
		float cost = resource.baseCost;
		float villagerBonus = villager.EffectiveDiplomacy * TRADE_DISCOUNT;
		if (mode == Sell)
		{
			cost *= SELL_VALUE_MULTIPLIER;
			cost *= 1 + villagerBonus;
		}
		else
		{
			cost *= 1 - villagerBonus;
		}
		return Mathf.RoundToInt(cost);
	}

	public SaveData Save()
	{
		var data = new SaveData();
		data.resourceName = resource.name;
		data.tradeMode = mode;
		return data;
	}

	[Serializable]
	public class SaveData
	{
		public string resourceName;
		public TradeMode tradeMode;
	}
}