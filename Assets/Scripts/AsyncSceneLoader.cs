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

	[SerializeField]
	private ThreadPriority loadingPriority;

	private AsyncOperation loading;
	private bool changeScene = false;

	private void Start()
	{
		StartCoroutine(IStartLoading(sceneName, delay));
	}

	public IEnumerator IStartLoading(string sceneName, float delay)
	{
		yield return new WaitForSeconds(delay);
		var prevPriority = Application.backgroundLoadingPriority;
		Application.backgroundLoadingPriority = loadingPriority;
		loading = SceneManager.LoadSceneAsync(sceneName);
		loading.completed += (loading) => Application.backgroundLoadingPriority = prevPriority;
		loading.priority = -1;
		loading.allowSceneActivation = changeScene;
		StartCoroutine(IChangeScene(prevPriority));
	}

	public void AllowChangingScene()
	{
		changeScene = true;
	}

	private IEnumerator IChangeScene(ThreadPriority prevPriority)
	{
		yield return new WaitUntil(() => changeScene);
		loading.allowSceneActivation = true;
	}
}
