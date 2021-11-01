using System;
using UnityEngine;
using Village.Scriptables;
using static Village.Controllers.GameController;
using static TradeOffer.TradeMode;

[Serializable]
public class TradeOffer
{
	public enum TradeMode { Buy, Sell }

	public TradeMode mode;
	public Resource resource;

	public int GetCost(Villager villager)
	{
		float cost = resource.baseCost;
		float villagerBonus = villager.Diplomacy * TRADE_DISCOUNT;
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
}