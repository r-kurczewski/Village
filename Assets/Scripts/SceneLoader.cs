using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
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

	public void ExitGame()
	{
		Application.Quit();
	}
}
