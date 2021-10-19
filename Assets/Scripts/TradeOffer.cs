using System;
using UnityEngine;

[Serializable]
public class TradeOffer
{
	public enum TradeMode { Buy, Sell}

	public TradeMode mode;
	public ResourceAmount yourResources;
	public ResourceAmount merchantResources;
}