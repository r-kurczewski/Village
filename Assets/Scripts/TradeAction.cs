using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action", menuName = "Village/Action/TradeAction")]
public class TradeAction : Action
{
	public TradeTarget target;
	public override void Apply(Villager target)
	{
		throw new System.NotImplementedException();
	}

	public enum TradeTarget { Merchant, CountryA, CountryB }
}
