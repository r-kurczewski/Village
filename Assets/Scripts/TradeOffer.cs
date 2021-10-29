using System;
using UnityEngine;
using static Village.Scriptables.Resource;

[Serializable]
public class TradeOffer
{
	public enum TradeMode { Buy, Sell}

	public TradeMode mode;
	public ResourceAmount yourResources;
	public ResourceAmount merchantResources;
}