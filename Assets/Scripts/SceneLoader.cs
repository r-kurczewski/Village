using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Village.Controllers;

public class SceneLoader : MonoBehaviour
{
	private const string localeStartNewGamePrompt = "prompt/startNewGame";

	[SerializeField]
	private PromptWindow prompt;

	[SerializeField]
	private Transform canvas;

	public void LoadGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}

	public void LoadProlog()
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
			var window = Instantiate(prompt, canvas);
			var message = LeanLocalization.GetTranslationText(localeStartNewGamePrompt);
			window.SetMessage(message);
			window.OnAccept.AddListener(() =>
			{
				SaveController.ClearSave();
				LoadProlog();
				window.Close();
			});
			window.OnDecline.AddListener(() =>
			{
				window.Close();
			});
		}
		else
		{
			SaveController.ClearSave();
			LoadProlog();
		}
	}

	public void LoadPreviousGame()
	{
		SaveController.LoadSaveData();
		LoadGameScene();
	}
}
