using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using VillageAnalyticsModel;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Text;
using System.IO;

#if UNITY_WEBGL && !UNITY_EDITOR

using System.Net;

#endif

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

			if (host == default)
			{
				Debug.LogWarning($"Could not send analytics data: Could not get a target hostname.");
				yield break;
			}

			var bytes = Encoding.UTF8.GetBytes(json);
			var compressBytes = CompressGZIP(bytes);

			using var request = UnityWebRequest.Put(host, compressBytes);
			request.SetRequestHeader("Content-Type", "application/json");
			request.SetRequestHeader("Content-Encoding", "gzip");

			var asyncOperation = request.SendWebRequest();

			yield return asyncOperation;

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogWarning($"Could not send analytics data: {request.error}");
			}
			else
			{
				Debug.Log("Analytics data sent.");
			}
		}

		private byte[] CompressGZIP(byte[] data)
		{
			using var compressedStream = new MemoryStream();
			using var zipStream = new GZipStream(compressedStream, CompressionMode.Compress);

			zipStream.Write(data, 0, data.Length);
			zipStream.Close();
			return compressedStream.ToArray();
		}
	}
}
