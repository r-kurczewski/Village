using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Serialization;
using static Village.Controllers.GameController;
using static Village.Controllers.GameController.GameDifficulty;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "GameChapter", menuName = "Village/GameChapter")]
	public class GameChapter : ScriptableObject
	{
		public Color color;

		[SerializeField]
		private string localeChapterName;

		public int chapterTurnStart;
		public AudioClip chapterMusic;
		public GameChapter nextChapter;

		[SerializeField]
		public Message chapterStartMessage;

		public List<PeriodicEvent> periodicEvents;

		public List<EventTimeSpan> events;

		public string ChapterName => Lean.Localization.LeanLocalization.GetTranslationText(localeChapterName);

		public List<GameEvent> GenerateEventList()
		{
			List<GameEvent> list = new List<GameEvent>();

			foreach (var timeSpanEvent in events)
			{
				//if (!timeSpanEvent.hardMode || instance.HardMode)
				//{
					int randDelay = Random.Range(0, timeSpanEvent.possibleDelay + 1);
					int eventTurn = chapterTurnStart + timeSpanEvent.turn + randDelay;
					list.Add(new GameEvent(timeSpanEvent.eventBase, eventTurn));
				//}
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
}