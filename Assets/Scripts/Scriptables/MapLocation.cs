using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "MapLocation", menuName = "Village/Location/MapLocation")]
	public class MapLocation : ScriptableObject
	{
		//public List<Action> basicActions = new List<Action>();

		[FormerlySerializedAs("basicActions2")]
		public List<ActionData> basicActions = new List<ActionData>();

		[Serializable]
		public class ActionData
		{
			public Action action;
			public bool hideInHardMode;

			public ActionData() { }

			public ActionData(Action action, bool hideInHardMode)
			{
				this.action = action;
				this.hideInHardMode = hideInHardMode;
			}
		}
	}
}