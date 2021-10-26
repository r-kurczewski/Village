using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "TradeEffect", menuName = "Village/Effect/TradeEffect")]
	public class TradeEffect : Effect
	{
		public override void Apply(int value, Villager villager)
		{
			// Implemented in TradeAction
			return;
		}
	}
}