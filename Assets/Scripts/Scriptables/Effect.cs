using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Effect : ScriptableObject
{
	public Sprite icon;
	public Color color = Color.white;

	public abstract void Apply(Villager villager, int value);
}
