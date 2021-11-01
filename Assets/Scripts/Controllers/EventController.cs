using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Village.Scriptables;
using Village.Views;
using static Village.Controllers.GameController;

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
		private EventBase merchantEvent;

		[SerializeField]
		private List<EventView> currentEvents = new List<EventView>();

		[SerializeField]
		private List<GameEvent> chapterEvents;

		[SerializeField]
		private int predictionFactor;

		public int PredictionFactor { get => predictionFactor; set => predictionFactor = value; }

		public EventView AddEvent(GameEvent gameEvent)
		{
			var view = Instantiate(eventPrefab, contentParent);
			view.Load(gameEvent);
			currentEvents.Add(view);
			return view;
		}

		public bool MerchantAvailable()
		{
			return currentEvents.Select(x => x.Event.eventBase).Contains(merchantEvent);
		}

		public void LoadChapterEvents()
		{
			GameChapter chapter = instance.Chapter;
			chapterEvents = chapter.GenerateEventList().OrderBy(x => x.turn).ToList();
		}

		public void EventUpdate()
		{
			int turnToLoad = instance.GetCurrentTurn() + predictionFactor;
			var newEvents = chapterEvents.Where(x => x.turn == turnToLoad);
			foreach (var newEvent in newEvents)
			{
				AddEvent(newEvent);
			}

			var toRemove = new List<EventView>();
			foreach (var ev in currentEvents)
			{
				int turnsLeft = ev.Event.turn + ev.Event.eventBase.turnDuration - turnToLoad;

				if (turnsLeft == 0)
				{
					bool eventSuccess = true;
					foreach (var req in ev.Event.eventBase.requirements)
					{
						if (instance.GetResourceAmount(req.resource) < req.amount)
						{
							eventSuccess = false;
						}
					}
					if (eventSuccess)
					{
						ev.Event.eventBase.requirements.ForEach(x => x.resource.Apply(-x.amount));
					}
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
