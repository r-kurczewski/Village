using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;
using static Village.Controllers.GameController;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "Resource", menuName = "Village/Resource")]
	public class Resource : Effect
	{
		public string resourceName;
		public int baseCost;
		public bool tradable = true;

		public override void Apply(int value, Villager villager = null)
		{
			instance.AddRemoveResource(this, value);
		}

		[Serializable]
		public class ResourceAmount
		{
			public Resource resource;

			[SerializeField]
			private int amount;

			public int Amount 
			{ 
				get
				{
					return amount;
				}
				set
				{
					amount = Mathf.Clamp(value, 0, RESOURCES_MAX); 
				}
			}

			public ResourceAmount(Resource resource, int amount)
			{
				this.resource = resource;
				this.amount = amount;
			}
		}
	}
}