using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Village/Resource")]
public class Resource : Effect
{
	public string resourceName;
	public int baseValue;

	public override void Apply(Villager villager, int value)
	{
		GameController.instance.AddRemoveResource(this, value);
	}
}