using System.Linq;
using UnityEngine;
using Village.Controllers;

namespace Village
{
	public class StartupConfig : MonoBehaviour
	{
		static bool completed = false;

		[SerializeField]
		private bool saveEncryption;

		private void Awake()
		{
			if (!completed)
			{
				SaveController.Encryption = saveEncryption;
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
