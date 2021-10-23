using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
	public int turn;
	public int predictionFactor;

	[Space(10)]
	public List<ResourceAmount> resources;

	[Space(10)]
	public List<Villager> villagers;

	[Space(10)]
	public List<EventView> events;

	[Space(10)]
	public List<TradeOffer> merchantOffer;
	public List<TradeOffer> countryAOffer;
	public List<TradeOffer> countryBOffer;
}