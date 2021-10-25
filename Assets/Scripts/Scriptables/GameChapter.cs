using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventBase;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "GameChapter", menuName = "Village/GameChapter")]
public class GameChapter : ScriptableObject
{
	public Color color;
	public string chapterName;
	public int chapterTurnStart;
	public AudioClip chapterMusic;
	public GameChapter nextChapter;

	[TextArea(5, 8)]
	public string chapterStartMessage;

	public List<PeriodicEvent> periodicEvents;

	public List<EventTimeSpan> events;

	public List<GameEvent> GenerateEventList()
	{
		List<GameEvent> list = new List<GameEvent>();
		foreach (var timeSpanEvent in events)
		{
			int randDelay = Random.Range(0, timeSpanEvent.possibleDelay + 1);
			int eventTurn = chapterTurnStart + timeSpanEvent.turn + randDelay;
			list.Add(new GameEvent(timeSpanEvent.eventBase, eventTurn));
		}
		foreach (var periodEvent in periodicEvents)
		{
			int eventTurn = periodEvent.turn;
			int duration = periodEvent.eventBase.turnDuration;

			while (chapterTurnStart + eventTurn + duration <= nextChapter.chapterTurnStart)
			{
				list.Add(new GameEvent(periodEvent.eventBase, chapterTurnStart + eventTurn));

				if (periodEvent.period == 0)
				{
					Debug.LogError("Period event with period of zero!", this);
					break;
				}
				eventTurn += periodEvent.period;
			}
		}
		return list;
	}
}
