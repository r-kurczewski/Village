using Lean.Localization;
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
	private const string languageString = "language";
	private const string musicVolumeString = "musicVolume";
	private const string resolutionString = "resolution";
	private const string fullscreenString = "fullscreen";

	[SerializeField]
	private TMP_Dropdown resolution;

	[SerializeField]
	private TMP_Dropdown language;

	[SerializeField]
	private Toggle fullscreen;

	[SerializeField]
	private Slider music;

	private void Start()
	{
		LoadResolutions();
		LoadMusicVolume();
		LoadLanguages();
	}

	public void LoadResolutions()
	{
		resolution.ClearOptions();
		var resolutions = Screen.resolutions.Select(x => $"{x.width}x{x.height}").ToList();
		resolution.AddOptions(resolutions);
		//var currentResolution = $"{Screen.width}x{Screen.height}";
		var currentResolution = PlayerPrefs.GetString("resolution");
		var currentResolutionIndex = resolution.options.IndexOf(resolution.options.FirstOrDefault(x => x.text == currentResolution));
		resolution.SetValueWithoutNotify(currentResolutionIndex);
		fullscreen.isOn = PlayerPrefs.GetInt("fullscreen") != 0;
	}

	public void LoadLanguages()
	{
		var langNames = LeanLocalization.CurrentLanguages.Keys.Select(x => new TMP_Dropdown.OptionData(x));
		var selectedLang = LeanLocalization.Instances.First().CurrentLanguage;
		language.options.AddRange(langNames);
		language.SetValueWithoutNotify(language.options.IndexOf(language.options.First(x => x.text == selectedLang)));
	}

	public void LoadMusicVolume()
	{
		music.value = PlayerPrefs.GetFloat(musicVolumeString);
	}

	public void ChangeResolution()
	{
		var option = resolution.options[resolution.value];
		var res = option.text.Split('x').Select(x => int.Parse(x)).ToArray();
		PlayerPrefs.SetString(resolutionString, option.text);
		PlayerPrefs.SetInt(fullscreenString, fullscreen.isOn ? 1 : 0);
		Screen.SetResolution(res[1], res[1], fullscreen.isOn);
	}

	public void ChangeVolume()
	{
		AudioListener.volume = music.value;
		PlayerPrefs.SetFloat(musicVolumeString, music.value);
	}

	public void ChangeLanguage()
	{
		string lang = language.options[language.value].text;
		PlayerPrefs.SetString(languageString, lang);
		LeanLocalization.SetCurrentLanguageAll(lang);
	}
}

