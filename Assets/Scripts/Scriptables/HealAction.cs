using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "HealAction", menuName = "Village/Action/HealAction")]
	public class HealAction : ResourceAction
	{
		[SerializeField]
		private Effect heal;

		public int HealStrength => effects.FirstOrDefault(x => x.effect == heal).value;

		public override IEnumerator Execute(Villager target)
		{
			if (IsCostCorrect())
			{
				ApplyCosts();
				target.Health += HealStrength;
			}
			yield break;
		}
	}
}