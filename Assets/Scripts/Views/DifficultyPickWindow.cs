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
			SetDifficulty(GameDifficulty.EasyWeakerEvents);
			LoadGame();
		}

		public void LoadEasy2()
		{
			SetDifficulty(GameDifficulty.EasyLessEvents);
			LoadGame();
		}

		private void LoadGame()
		{
			SaveController.ClearSave();
			SceneLoader.LoadProlog();
		}

		public void LoadHard()
		{
			SetDifficulty(GameDifficulty.Hard);
			LoadGame();
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
