using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
	[Header("Basic")]
	public int turn;

	[Header("Current resources")]
	public List<ResourceQuantity> resources;

	[Header("Current reputation")]
	public int CountryAReputation;
	public int CountryBReputation;

	[Header("Villagers")]
	public List<Villager> villagers;

	[Header("Active events")]
	public List<Event> events;
	
	[Header("Current trades")]
	public List<TradeOffer> merchantOffer;
	public List<TradeOffer> countryAOffer;
	public List<TradeOffer> countryBOffer;
}