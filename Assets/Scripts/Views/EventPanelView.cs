using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EventPanelView : MonoBehaviour
{
	[SerializeField]
	private Transform contentParent;

	[SerializeField]
	private EventView eventPrefab;

	public List<EventView> events = new List<EventView>();

	public EventView AddEvent(GameEvent gameEvent)
	{
		var view = Instantiate(eventPrefab, contentParent);
		view.Load(gameEvent);
		events.Add(view);
		return view;
	}
}
