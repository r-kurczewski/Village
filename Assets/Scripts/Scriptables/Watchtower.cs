using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "Watchtower", menuName = "Village/Location/Watchtower")]
	public class Watchtower : MapBuilding
	{
		public override void ApplyOnetimeBonus()
		{
			Debug.Log("Prediction");
			GameController.instance.SetPredictionFactor(1);
		}
	}
}