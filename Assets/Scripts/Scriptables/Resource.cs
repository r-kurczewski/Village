using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "Resource", menuName = "Village/Resource")]
	public class Resource : Effect
	{
		public string resourceName;
		public int baseValue;

		public override void Apply(int value, Villager villager = null)
		{
			GameController.instance.AddRemoveResource(this, value);
		}

		[Serializable]
		public class ResourceAmount
		{
			public Resource resource;
			public int amount;

			public ResourceAmount(Resource resource, int amount)
			{
				this.resource = resource;
				this.amount = amount;
			}
		}
	}
}