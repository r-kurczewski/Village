using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Village.Scriptables;
using Village.Views;

namespace Village.Controllers
{
	[SelectionBase]
	public class TurnController : MonoBehaviour
	{
		[SerializeField]
		private TurnView view;

		[SerializeField]
		private Image panel;

		[SerializeField]
		private GameChapter chapter;

		[SerializeField]
		private int turn = 1;

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
					panel.color = selected.color;
					view.SetChapterName(selected.chapterName);
					GameController.instance.PlayMusic(selected.chapterMusic);
					break;
				}
				else selected = selected.nextChapter;
			}
		}

		public void CheckIfGameEnds()
		{
			if (chapter.nextChapter is null
				&& Turn != Chapter.chapterTurnStart)
			{
				SceneManager.LoadScene("MainMenu");
			}
		}

		public void MoveToNextTurn()
		{
			turn++;
		}

		public void RefreshGUI()
		{
			view.SetTurn(turn);
		}
	}
}
