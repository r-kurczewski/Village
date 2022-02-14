using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Village.Scriptables;
using Village.Views;
using static Village.Controllers.GameController;

namespace Village.Controllers
{
	[SelectionBase]
	public class TurnController : MonoBehaviour
	{
		[SerializeField]
		private GameChapter chapter;

		[SerializeField]
		private int turn = 1;

		[SerializeField]
		private UnityData references;

		public int Turn => turn;

		public GameChapter Chapter => chapter;

		public void ChapterUpdate()
		{
			GameChapter selected = chapter;
			while (selected != null)
			{
				// New chapter begins
				if (turn == selected.chapterTurnStart)
				{
					chapter = selected;
					references.panel.color = selected.color;
					references.view.SetChapterName(selected.ChapterName);
					instance.PlayMusic(selected.chapterMusic);
					LoadChapterMessage(chapter.chapterStartMessage);
					instance.LoadChapterEvents();
					instance.ApplyIntelligenceBonus();
					break;
				}
				else selected = selected.nextChapter;
			}
		}

		public void CheckIfGameEnds()
		{
			bool lastTurn = chapter.nextChapter is null && Turn != Chapter.chapterTurnStart;
			int villagersCount = instance.GetVillagersCount();
			if (villagersCount == 0 || lastTurn)
			{
				int reputationA = instance.GetResourceAmount(references.countryAReputation);
				int reputationB = instance.GetResourceAmount(references.countryBReputation);
				EndingLoader.ending = SelectEnding(villagersCount, reputationA, reputationB);
				SceneManager.LoadScene("EndingScene");
			}
		}

		public void LoadChapterMessage(Message message)
		{
			var view = Instantiate(references.messagePrefab, references.canvas);
			view.LoadMessage(message);
			view.MessagesEnded.AddListener(() => Destroy(view.gameObject));
		}

		private Message SelectEnding(int villagersCount, int reputationA, int reputationB)
		{
			Message ending = default;

			if (villagersCount == 0)
			{
				ending = references.ending1;
			}
			else
			{
				if (reputationA >= NEUTRAL_ENDING_REPUTATION
				&& reputationB >= NEUTRAL_ENDING_REPUTATION)
				{
					ending = references.ending5;
				}
				else if (reputationB >= COUNTRY_B_ENDING_REPUTATION)
				{
					ending = references.ending4;
				}
				else if (reputationA >= COUNTRY_A_ENDING_REPUTATION)
				{
					ending = references.ending3;
				}
				else
				{
					ending = references.ending2;
				}
			}
			return ending;
		}

		public void MoveToNextTurn()
		{
			turn++;
		}

		public void RefreshGUI()
		{
			references.view.SetTurn(turn);
		}

		[Serializable]
		public class UnityData
		{
			public TurnView view;

			public Image panel;

			public Message
				ending1,
				ending2,
				ending3,
				ending4,
				ending5;

			public Resource countryAReputation;

			public Resource countryBReputation;

			public MessageView messagePrefab;

			public Transform canvas;
		}
	}
}
