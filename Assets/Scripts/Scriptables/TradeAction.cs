using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Village.Controllers.GameController;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "TradeAction", menuName = "Village/Action/TradeAction")]
	public class TradeAction : Action
	{
		public enum TradeTarget { Merchant, CountryA, CountryB }
		public TradeTarget target;

		public override void Execute(Villager target)
		{
			if (!target) return;

			instance.LoadTradeWindow(target);
		}

		public override float GetMultiplier(Villager villager)
		{
			if (villager is null) return 0;
			return villager.Diplomacy * TRADE_DISCOUNT * 100;
		}
	}
}