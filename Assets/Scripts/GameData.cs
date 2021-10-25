using System;
using System.Collections.Generic;
using UnityEngine;
using static GameChapter;
using static EventBase;

[Serializable]
public class GameData
{
	public GameChapter chapter;
	public int currentTurn = 1;
	public int predictionFactor = 0;

	[Space(10)]

	public List<ResourceAmount> resources;

	[Space(10)]

	public List<Villager> villagers;

	[Space(10)]

	public List<GameEvent> chapterEvents;

	[Space(10)]

	public List<TradeOffer> merchantOffer;
	public List<TradeOffer> countryAOffer;
	public List<TradeOffer> countryBOffer;
}