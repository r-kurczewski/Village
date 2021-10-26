using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "VillagerBase", menuName = "Village/VillagerBase")]
	public class VillagerBase : ScriptableObject
	{
		public string villagerName;
		public Sprite avatar;

		[Range(-3, 3)]
		public int CountryAReputation, CountryBReputation;

		[Range(0, 5)]
		public int
			baseStrength,
			baseGathering,
			baseCrafting,
			baseDiplomacy,
			baseIntelligence;
	}
}