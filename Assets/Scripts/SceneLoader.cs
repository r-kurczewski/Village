using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Village.Controllers;

public class SceneLoader : MonoBehaviour
{
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
		SceneManager.LoadScene("Assets");
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
		SaveController.save = null;
		LoadProlog();
	}

	public void LoadPreviousGame()
	{
		SaveController.LoadSaveData();
		LoadGameScene();
	}
}
