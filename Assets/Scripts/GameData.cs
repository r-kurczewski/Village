using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
	[Header("Basic")]
	public int turn;
	public int predictionFactor;

	[Header("Current resources")]
	public List<ResourceAmount> resources;

	[Header("Villagers")]
	public List<Villager> villagers;

	[Header("Active events")]
	public List<EventView> events;

	[Header("Current trades")]
	public List<TradeOffer> merchantOffer;
	public List<TradeOffer> countryAOffer;
	public List<TradeOffer> countryBOffer;
}