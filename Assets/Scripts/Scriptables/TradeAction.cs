using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TradeAction", menuName = "Village/Action/TradeAction")]
public class TradeAction : Action
{
	public enum TradeTarget { Merchant, CountryA, CountryB }
	public TradeTarget target;

	public override void Execute(Villager target)
	{
		throw new System.NotImplementedException();
	}


}
