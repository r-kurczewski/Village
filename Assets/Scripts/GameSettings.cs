using Lean.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Village.Controllers;

public class GameSettings : MonoBehaviour
{
	private const string languageString = "language";
	private const string masterVolumeString = "masterVolume";
	private const string musicVolumeString = "musicVolume";
	private const string effectsVolumeString = "effectsVolume";
	private const string resolutionString = "resolution";
	private const string fullscreenString = "fullscreen";
	private const string hideTooltipsString = "hideAdvancedTooltips";

	[SerializeField]
	private TMP_Dropdown resolution;

	[SerializeField]
	private TMP_Dropdown language;

	[SerializeField]
	private Toggle fullscreen;

	[SerializeField]
	private Slider master;

	[SerializeField]
	private Slider music;

	[SerializeField]
	private Slider effects;

	[SerializeField]
	private Toggle hideTooltips;

	private void Start()
	{
		LoadResolutions();
		LoadLanguages();
		LoadHideTooltips();

		LoadMasterVolume();
		LoadMusicVolume();
		LoadEffectsVolume();
	}
	public static bool SimplifiedTooltips => PlayerPrefs.GetInt(hideTooltipsString, defaultValue: 0) != 0;

	public static bool FullScreen => PlayerPrefs.GetInt("fullscreen", defaultValue: Screen.fullScreen ? 1 : 0) != 0;

	public static float MasterVolume => PlayerPrefs.GetFloat(masterVolumeString, defaultValue: 1);

	public static float MusicVolume => PlayerPrefs.GetFloat(musicVolumeString, defaultValue: 1);

	public static float EffectsVolume => PlayerPrefs.GetFloat(effectsVolumeString, defaultValue: 1);

	private void LoadHideTooltips()
	{
		hideTooltips.isOn = SimplifiedTooltips;
	}

	private void LoadResolutions()
	{
		resolution.ClearOptions();
		var resolutions = Screen.resolutions.Select(x => $"{x.width}x{x.height}").ToList();
		resolution.AddOptions(resolutions);
		var currentResolution = PlayerPrefs.GetString("resolution");
		var currentResolutionIndex = resolution.options.IndexOf(resolution.options.FirstOrDefault(x => x.text == currentResolution));
		resolution.SetValueWithoutNotify(currentResolutionIndex);
		fullscreen.isOn = FullScreen;
	}

	private void LoadLanguages()
	{
		var langNames = LeanLocalization.CurrentLanguages.Keys.Select(x => new TMP_Dropdown.OptionData(x));
		var selectedLang = LeanLocalization.Instances.First().CurrentLanguage;
		language.options.AddRange(langNames);
		language.SetValueWithoutNotify(language.options.IndexOf(language.options.First(x => x.text == selectedLang)));
	}

	private void LoadMasterVolume()
	{
		float volume = MasterVolume;
		master.value = volume;
		AudioController.instance.SetMasterVolume(volume);
	}

	private void LoadMusicVolume()
	{
		float volume = MusicVolume;
		music.value = volume;
		AudioController.instance.SetMusicVolume(volume);
	}

	private void LoadEffectsVolume()
	{
		float volume = EffectsVolume;
		effects.value = volume;
		AudioController.instance.SetEffectsVolume(volume);
	}

	public void ChangeResolution()
	{
		var option = resolution.options[resolution.value];
		var res = option.text.Split('x').Select(x => int.Parse(x)).ToArray();
		PlayerPrefs.SetString(resolutionString, option.text);
		PlayerPrefs.SetInt(fullscreenString, fullscreen.isOn ? 1 : 0);
		Screen.SetResolution(res[0], res[1], fullscreen.isOn);
	}

	public void ChangeMasterVolume()
	{
		float volume = master.value;
		AudioController.instance.SetMasterVolume(volume);
		PlayerPrefs.SetFloat(masterVolumeString, volume);
	}

	public void ChangeMusicVolume()
	{
		float volume = music.value;
		AudioController.instance.SetMusicVolume(volume);
		PlayerPrefs.SetFloat(musicVolumeString, volume);
	}

	public void ChangeEffectsVolume()
	{
		float volume = effects.value;
		AudioController.instance.SetEffectsVolume(volume);
		PlayerPrefs.SetFloat(effectsVolumeString, volume);
	}

	public void ChangeLanguage()
	{
		string lang = language.options[language.value].text;
		PlayerPrefs.SetString(languageString, lang);
		LeanLocalization.SetCurrentLanguageAll(lang);
	}

	public void ChangeAdavancedTooltips()
	{
		PlayerPrefs.SetInt(hideTooltipsString, hideTooltips.isOn ? 1 : 0);
	}
}

