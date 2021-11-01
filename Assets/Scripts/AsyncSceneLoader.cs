using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
	[SerializeField]
	private string sceneName;

	[SerializeField]
	private float delay;

	private AsyncOperation loading;
	bool changeScene = false;

	private void Start()
	{
		StartCoroutine(IStartLoading(sceneName, delay));
	}

	public IEnumerator IStartLoading(string sceneName, float delay)
	{
		yield return new WaitForSeconds(delay);
		loading = SceneManager.LoadSceneAsync(sceneName);
		loading.allowSceneActivation = changeScene;
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
