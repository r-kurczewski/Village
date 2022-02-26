using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "Stat", menuName = "Village/VillagerStat")]
	public class VillagerStat : ScriptableObject
	{
		public Sprite icon;
		public Sprite backgroundIcon;
		public Color color;
	}
}