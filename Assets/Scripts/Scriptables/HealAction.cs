using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="HealAction", menuName = "Village/Action/HealAction")]
public class HealAction : ResourceAction
{
	[SerializeField]
	private Effect heal;

	public int HealStrength => effects.FirstOrDefault(x => x.effect == heal).amount;

	public override void Apply(Villager target)
	{
		target.health = Mathf.Clamp(target.health + HealStrength, 0, 4);
	}
}
