using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EffectAmount
{
	public Effect effect;
	public int amount;

	public EffectAmount(Effect effect, int amount)
	{
		this.effect = effect;
		this.amount = amount;
	}

	public static explicit operator EffectAmount(ResourceAmount resAm) => new EffectAmount(resAm.resource, resAm.amount);
}
