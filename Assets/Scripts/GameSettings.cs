using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
	[SerializeField]
	private TMP_Dropdown resolution;

	[SerializeField]
	private Toggle fullscreen;

	[SerializeField]
	private Slider music;

	private void Start()
	{
		LoadResolutions();
	}

	public void LoadResolutions()
	{
		var resolutionStrings = Screen.resolutions.
			Select(x => $"{x.width}x{x.height}").ToList();
		resolution.ClearOptions();
		resolution.AddOptions(resolutionStrings);
	}

	public void ChangeResolution()
	{
		var res = resolution.options[resolution.value].text.Split('x');
		Debug.Log("Set resolution to: " + res[0] + "x" + res[1]);
		Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), fullscreen.isOn);
	}

	public void ChangeVolume()
	{
		AudioListener.volume = music.value;
	}
}

