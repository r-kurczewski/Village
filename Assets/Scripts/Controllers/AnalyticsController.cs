using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using VillageAnalyticsModel;
using System;
using Newtonsoft.Json;

namespace Village.Controllers
{
	public class AnalyticsController : MonoBehaviour
	{
		public void SendGameAnalyticsData(GameAnalyticsData data)
		{
			#if UNITY_WEBGL && !UNITY_EDITOR
			StartCoroutine(SendGameAnalyticsCoroutine(data));
			#endif
		}

		private IEnumerator SendGameAnalyticsCoroutine(GameAnalyticsData data)
		{

			var json = JsonConvert.SerializeObject(data);
			var host = (string)GameController.instance.Config?["AnalyticsUrl"];

			if(host == default)
			{
				Debug.LogWarning($"Could not send analytics data: Could not get a target hostname.");
				yield break;
			}

			using var request = UnityWebRequest.Put(host, json);
			request.SetRequestHeader("Content-Type", "application/json");

			var asyncOperation = request.SendWebRequest();

			yield return asyncOperation;

			if(request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogWarning($"Could not send analytics data: {request.error}");
			}
			else
			{
				Debug.Log("Analytics data sent.");
			}
		}
	}
}
