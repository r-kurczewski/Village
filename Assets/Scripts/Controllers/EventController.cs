using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Village.Scriptables;
using Village.Views;

namespace Village.Controllers
{
	[SelectionBase]
	public class EventController : MonoBehaviour
	{
		[SerializeField]
		private Transform contentParent;

		[SerializeField]
		private EventView eventPrefab;

		[SerializeField]
		private List<EventView> currentEvents = new List<EventView>();

		[SerializeField]
		private List<GameEvent> chapterEvents;

		[SerializeField]
		private int predictionFactor;

		public int PredictionFactor { get; set; }

		public EventView AddEvent(GameEvent gameEvent)
		{
			var view = Instantiate(eventPrefab, contentParent);
			view.Load(gameEvent);
			currentEvents.Add(view);
			return view;
		}

		public void LoadChapterEvents()
		{
			GameChapter chapter = GameController.instance.Chapter;
			chapterEvents = chapter.GenerateEventList().OrderBy(x => x.turn).ToList();
		}

		public void EventUpdate()
		{
			int currentTurn = GameController.instance.GetCurrentTurn();
			var newEvents = chapterEvents.Where(x => x.turn == currentTurn );
			foreach (var newEvent in newEvents)
			{
				AddEvent(newEvent);
			}

			var toRemove = new List<EventView>();
			foreach (var ev in currentEvents)
			{
				int turnsLeft = ev.Event.turn + ev.Event.eventBase.turnDuration - currentTurn;

				if (turnsLeft == 0)
				{
					bool eventSuccess = true;
					foreach (var req in ev.Event.eventBase.requirements)
					{
						if (GameController.instance.GetResourceAmount(req.resource) < req.amount)
						{
							eventSuccess = false;
						}
					}
					if (eventSuccess)
					{
						ev.Event.eventBase.requirements.ForEach(x => x.resource.Apply(-x.amount));
					}
					Debug.Log($"Event {ev.Event.eventBase.title}: {eventSuccess}");
					if (eventSuccess)
					{
						ev.Event.eventBase.ApplySuccess();
					}
					else
					{
						ev.Event.eventBase.ApplyFailure();
					}
					toRemove.Add(ev);

				}
				else
				{
					ev.SetTurnLeft(turnsLeft);
				}
			}
			foreach (var ev in toRemove)
			{
				currentEvents.Remove(ev);
				Destroy(ev.gameObject);
			}
		}
	}
}
