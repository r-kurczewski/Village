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
		public override IEnumerator Execute(Villager target)
		{
			if (!target) yield break;

			instance.LoadTradeWindow(target);

			yield return new WaitWhile(() => instance.GetTradeActive());
		}

		public override float GetMultiplier(Villager villager)
		{
			if (villager is null) return 0;
			return villager.EffectiveDiplomacy * TRADE_DISCOUNT * 100;
		}
	}
}