using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using static EventBase;

public class GameController : MonoBehaviour
{
	public static GameController instance;

	[SerializeField]
	private ResourcePanelView resourcePanel;

	[SerializeField]
	private VillagerPanelView villagerPanel;

	[SerializeField]
	private TurnView turnView;

	[SerializeField]
	private EventPanelView eventPanelView;

	[SerializeField]
	private Image rightPanel;

	[SerializeField]
	private AudioSource audioPlayer;

	[Space(10)]

	[SerializeField]
	private List<VillagerBase> availableVillagers;

	[SerializeField]
	private GameData data;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else Debug.LogWarning("There is more than one GameController in the scene!");
	}
	private void Start()
	{
		var randomVillagers = availableVillagers.OrderBy(x => UnityEngine.Random.value).Take(6);
		foreach (var villagerBase in randomVillagers)
		{
			data.villagers.Add(villagerPanel.CreateNewVillager(villagerBase));
		}

		data.chapterEvents = data.chapter.GenerateEventList().OrderBy(x => x.turn).ToList();

		TurnUpdate();
	}

	private void TurnUpdate()
	{
		ChapterUpdate();
		EventUpdate();
		UpdateGUI();
	}

	private void EventUpdate()
	{
		var newEvents = data.chapterEvents.Where(x => x.turn == data.currentTurn);
		foreach (var newEvent in newEvents)
		{
			eventPanelView.AddEvent(newEvent);
		}

		var events = eventPanelView.events;
		var toRemove = new List<EventView>();
		foreach (var ev in eventPanelView.events)
		{
			int currentTurn = data.currentTurn;
			int turnsLeft = ev.Event.turn + ev.Event.eventBase.turnDuration - currentTurn;

			if (turnsLeft > 0)
			{
				ev.SetTurnLeft(turnsLeft);
			}
			else
			{
				// TODO: Apply event effects
				toRemove.Add(ev);
			}
		}
		foreach (var ev in toRemove)
		{
			events.Remove(ev);
			Destroy(ev.gameObject);
		}
	}

	public void AddRemoveVillagersHealth(int value)
	{
		data.villagers.ForEach(x => x.health += value);
	}
	public void AddRemoveResource(Resource resource, int amount)
	{
		data.resources.First(x => x.resource == resource).amount += amount;
		resourcePanel.Refresh();
	}

	public int GetResourceAmount(Resource resource)
	{
		return data.resources.First(x => x.resource == resource).amount;
	}

	public int GetCurrentTurn()
	{
		return data.currentTurn;
	}

	public void EndTurn()
	{
		foreach (var villager in data.villagers)
		{
			villager.GetComponent<VillagerView>().MoveToVillagerPanel();
		}
		data.currentTurn++;
		CheckIfGameEnds();
		TurnUpdate();
	}

	public void SetPredictionFactor(int value)
	{
		data.predictionFactor = value;
	}

	public void UpdateGUI()
	{
		resourcePanel.Refresh();
		turnView.SetValue(data.currentTurn);
	}

	public void CheckIfGameEnds()
	{
		if (data.chapter.nextChapter is null
			&& data.currentTurn != data.chapter.chapterTurnStart)
		{
			SceneManager.LoadScene("MainMenu");
		}
	}

	private void ChapterUpdate()
	{
		GameChapter selected = data.chapter;
		while (selected != null)
		{
			// New chapter begins
			if (data.currentTurn == selected.chapterTurnStart)
			{
				data.chapter = selected;
				data.chapterEvents = data.chapter.GenerateEventList();
				rightPanel.color = selected.color;
				turnView.SetChapterName(selected.chapterName);
				audioPlayer.clip = selected.chapterMusic;
				audioPlayer.Play();
				break;
			}
			else selected = selected.nextChapter;
		}
	}
}