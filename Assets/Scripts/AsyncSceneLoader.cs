using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
	[SerializeField]
	private string sceneName;

	private AsyncOperation loading;
	bool changeScene = false;

	private void Start()
	{
		StartLoading(sceneName);
	}

	public void StartLoading(string sceneName)
	{
		loading = SceneManager.LoadSceneAsync(sceneName);
		loading.allowSceneActivation = false;
		StartCoroutine(IChangeScene());
	}

	public void AllowChangingScene()
	{
		changeScene = true;
	}

	private IEnumerator IChangeScene()
	{
		yield return new WaitUntil(() => changeScene);
		loading.allowSceneActivation = true;
	}
}
