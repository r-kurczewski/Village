using Lean.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using Village.Controllers;
using Village.Views;

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
		if (SaveController.IsCorrectSave)
		{
			var overwriteSaveWindow = Instantiate(newGamePrompt, canvas);
			var message = LeanLocalization.GetTranslationText(localeStartNewGamePrompt);
			overwriteSaveWindow.LoadMessage(message);
			overwriteSaveWindow.OnAccept.AddListener(() =>
			{
				LoadDiffciultyWindow();
				overwriteSaveWindow.Close();
			});
			overwriteSaveWindow.OnDecline.AddListener(() =>
			{
				overwriteSaveWindow.Close();
			});
			overwriteSaveWindow.RefreshLayout();
		}
		else
		{
			LoadDiffciultyWindow();
		}
	}

	private void LoadDiffciultyWindow()
	{
		difficultyWindow.Show();
	}

	public void LoadPreviousGame()
	{
		SaveController.LoadSaveData();
		LoadGameScene();
	}
}
