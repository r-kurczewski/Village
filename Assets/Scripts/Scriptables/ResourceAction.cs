using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "ResourceAction", menuName = "Village/Action/ResourceAction")]
	public class ResourceAction : Action
	{
		public override void Execute(Villager target)
		{
			float multiplier = GetVillagerMultiplier(target);
			if (IsCostCorrect())
			{
				ApplyCosts();
				ApplyEffects(multiplier);
			}
		}
	}

}