using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Village.Controllers;
using Village.Views;
using UnityEngine.Serialization;

public class SceneLoader : MonoBehaviour
{
	private const string localeStartNewGamePrompt = "prompt/startNewGame";

	[SerializeField]
	private PromptWindow newGamePrompt;

	[SerializeField]
	private DifficultyPickWindow difficultyWindow;

	[SerializeField]
	private Transform canvas;

	public void LoadGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}

	public static void LoadProlog()
	{
		SceneManager.LoadScene("PrologScene");
	}

	public void LoadEnding()
	{
		SceneManager.LoadScene("EndingScene");
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void LoadCreditsScene()
	{
		SceneManager.LoadScene("NewCredits");
	}

	public void LoadSettings()
	{
		SceneManager.LoadScene("Settings");
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void StartNewGame()
	{
		if (SaveController.SaveExists)
		{
			var window = Instantiate(newGamePrompt, canvas);
			var message = LeanLocalization.GetTranslationText(localeStartNewGamePrompt);
			window.LoadMessage(message);
			window.OnAccept.AddListener(() =>
			{
				LoadNewGame();
				window.Close();
			});
			window.OnDecline.AddListener(() =>
			{
				window.Close();
			});
			window.RefreshLayout();
		}
		else
		{
			SaveController.ClearSave();
			LoadProlog();
		}
	}

	private void LoadNewGame()
	{
		difficultyWindow.Show();
	}

	public void LoadPreviousGame()
	{
		SaveController.LoadSaveData();
		LoadGameScene();
	}
}
