using System;
using Village.Scriptables;
using UnityEngine.Serialization;

[Serializable]
public class GameEvent
{
	public EventBase eventBase;
	public int turn;

	public GameEvent()
	{

	}

	public GameEvent(EventBase eventBase, int turns)
	{
		this.eventBase = eventBase;
		this.turn = turns;
	}

	public SaveData Save()
	{
		var data = new SaveData();
		data.eventName = eventBase.name;
		data.turn = turn;
		return data;
	}

	[Serializable]
	public class SaveData
	{
		public string eventName;
		public int turn;
	}
}

[Serializable]
public class EventTimeSpan : GameEvent
{
	public int possibleDelay;
}

[Serializable]
public class PeriodicEvent : GameEvent
{
	public int period;
}

