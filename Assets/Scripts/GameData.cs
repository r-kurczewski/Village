using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
	public TradeWindow window;

	private void Start()
	{
		window.Load(merchantOffer);
	}

	[Header("Basic")]
	public int turn;

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