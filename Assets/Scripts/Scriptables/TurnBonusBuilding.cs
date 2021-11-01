using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;
using static Village.Scriptables.Resource;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "TurnBonusBuilding", menuName = "Village/Location/TurnBonusBuilding")]
	public class TurnBonusBuilding : MapBuilding
	{
		[SerializeField]
		private ResourceAmount resource;
		public override void ApplyTurnBonus()
		{
			GameController.instance.AddRemoveResource(resource.resource, resource.amount);
		}
	}
}