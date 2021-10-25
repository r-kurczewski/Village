using System;

[Serializable]
public class GameEvent
{
	public EventBase eventBase;
	public int turn;

	public GameEvent()
	{

	}

	public GameEvent(EventBase eventBase, int turn)
	{
		this.eventBase = eventBase;
		this.turn = turn;
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

