using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Village.Controllers;
using static Village.Controllers.GameController;

namespace Village.Views
{
	public class DifficultyPickWindow : MonoBehaviour
	{
		public void LoadEasy()
		{
			SetDifficulty(GameDifficulty.Easy);
			LoadGame();
		}

		public void LoadNormal()
		{
			SetDifficulty(GameDifficulty.Normal);
			LoadGame();
		}
		public void LoadHard()
		{
			SetDifficulty(GameDifficulty.Hard);
			LoadGame();
		}

		private void LoadGame()
		{
			SaveController.ClearSave();
			SceneLoader.LoadProlog();
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}
	}
}
