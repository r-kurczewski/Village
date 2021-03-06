using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Encoders;
using System.Linq;

namespace Village
{
	public class StartupConfig : MonoBehaviour
	{
		static bool completed = false;

		[SerializeField]
		private bool saveEncode;

		private void Awake()
		{
			if (!completed)
			{
				SaveGame.Encoder = new PlainEncoder();
				SaveGame.Encode = saveEncode;
				TryLoadOptimalResolution();
				completed = true;
            }
		}

		private void TryLoadOptimalResolution()
		{
			var savedResolution = PlayerPrefs.GetString(GameSettings.resolutionString, "");
			if (savedResolution == "")
			{
				var resolution = Screen.resolutions
					.OrderBy(x => x.width)
					.ThenBy(x => x.height)
					.Last();
				var resolutionString = $"{resolution.width}x{resolution.height}";
				Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
				PlayerPrefs.SetString(GameSettings.resolutionString, resolutionString);
				Debug.Log("Loading optimal resolution: " + resolutionString);
			}
		}
	}
}
