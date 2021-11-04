using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static Village.Scriptables.Resource;

namespace Village.Scriptables
{
	public abstract class Effect : ScriptableObject
	{
		public Sprite icon;
		public Color color = Color.white;

		public abstract void Apply(int value, Villager target = null);

		[Serializable]
		public class EffectAmount
		{
			public Effect effect;
			[FormerlySerializedAs("amount")]
			public int value;

			public EffectAmount(Effect effect, int amount)
			{
				this.effect = effect;
				this.value = amount;
			}

			public static explicit operator EffectAmount(ResourceAmount resAm) => new EffectAmount(resAm.resource, resAm.Amount);
		}
	}
}