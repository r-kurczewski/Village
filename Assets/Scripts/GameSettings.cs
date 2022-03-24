using Lean.Localization;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Village.Controllers;
using UnityEngine.Serialization;

public class GameSettings : MonoBehaviour
{
	public const string languageString = "language";
	public const string masterVolumeString = "masterVolume";
	public const string musicVolumeString = "musicVolume";
	public const string effectsVolumeString = "effectsVolume";
	public const string resolutionString = "resolution";
	public const string fullscreenString = "fullscreen";
	public const string hideTooltipsString = "hideAdvancedTooltips";
	public const string villagerReturnString = "returnVillagers";
	private const string showTipsString = "showTips";

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

	[SerializeField][FormerlySerializedAs("hideTooltips")]
	private Toggle hideDescription;

	[SerializeField]
	private Toggle showTips;

	private List<LocalizedLanguage> languageNames = new List<LocalizedLanguage>()
	{
		new LocalizedLanguage("Polish", "Polski"),
		new LocalizedLanguage("English", "English"),
	};

	public static string Resolution
	{
		get { return PlayerPrefs.GetString(resolutionString, defaultValue: string.Empty); }
		set { PlayerPrefs.SetString(resolutionString, value); }
	}

	public static string Language
	{
		get { return PlayerPrefs.GetString(languageString, defaultValue: string.Empty); }
		private set { PlayerPrefs.SetString(languageString, value); }
	}

	public static bool Fullscreen
	{
		get { return PlayerPrefs.GetInt(fullscreenString, defaultValue: Screen.fullScreen ? 1 : 0) != 0; }
		private set { PlayerPrefs.SetInt(fullscreenString, value ? 1 : 0); }
	}

	public static bool HideDescription
	{
		get { return PlayerPrefs.GetInt(hideTooltipsString, defaultValue: 0) != 0; }
		private set { PlayerPrefs.SetInt(hideTooltipsString, value ? 1 : 0); }
	}

	public static bool ShowTips
	{
		get { return PlayerPrefs.GetInt(showTipsString, defaultValue: 1) != 0; }
		set { PlayerPrefs.SetInt(showTipsString, value ? 1 : 0); }
	}

	public static float MasterVolume
	{
		get { return PlayerPrefs.GetFloat(masterVolumeString, defaultValue: 1); }
		private set { PlayerPrefs.SetFloat(masterVolumeString, value); }
	}

	public static float MusicVolume
	{
		get { return PlayerPrefs.GetFloat(musicVolumeString, defaultValue: 1); }
		private set { PlayerPrefs.SetFloat(musicVolumeString, value); }
	}

	public static float EffectsVolume
	{
		get { return PlayerPrefs.GetFloat(effectsVolumeString, defaultValue: 1); }
		private set { PlayerPrefs.SetFloat(effectsVolumeString, value); }
	}

	private void Start()
	{
		LoadResolutions();
		LoadLanguages();
		hideDescription.isOn = HideDescription;
		showTips.isOn = ShowTips;

		LoadMasterVolume();
		LoadMusicVolume();
		LoadEffectsVolume();
	}

	private void LoadResolutions()
	{
		resolution.ClearOptions();
		var resolutions = Screen.resolutions
			.Where(x => x.width >= 1024)
			.Where(x => x.height >= 768)
			.Select(x => $"{x.width}x{x.height}")
			.Distinct()
			.ToList();
		resolution.AddOptions(resolutions);
		var currentResolutionIndex = resolution.options.IndexOf(resolution.options.FirstOrDefault(x => x.text == Resolution));
		resolution.SetValueWithoutNotify(currentResolutionIndex);
		fullscreen.isOn = Fullscreen;
	}

	private void LoadLanguages()
	{
		var langNames = LeanLocalization.CurrentLanguages.Keys
			.Select(x => GetLocalizedLanguageName(x))
			.ToList();
		var selectedLang = LeanLocalization.Instances.First().CurrentLanguage;
		language.AddOptions(langNames);
		language.SetValueWithoutNotify(language.options.IndexOf(language.options.First(x => x.text == GetLocalizedLanguageName(selectedLang))));
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
		Resolution = option.text;
		Fullscreen = fullscreen.isOn;
		Screen.SetResolution(res[0], res[1], fullscreen.isOn);
	}

	public void ChangeMasterVolume()
	{
		float volume = master.value;
		AudioController.instance.SetMasterVolume(volume);
		MasterVolume = volume;
	}

	public void ChangeMusicVolume()
	{
		float volume = music.value;
		AudioController.instance.SetMusicVolume(volume);
		MusicVolume = volume;
	}

	public void ChangeEffectsVolume()
	{
		float volume = effects.value;
		AudioController.instance.SetEffectsVolume(volume);
		EffectsVolume = volume;
	}

	public void ChangeLanguage()
	{
		string lang = language.options[language.value].text;
		Language = lang;
		LeanLocalization.SetCurrentLanguageAll(GetEnglishLanguageName(lang));
	}

	public void ChangeAdvancedTooltips()
	{
		HideDescription = hideDescription.isOn;
	}

	public void ChangeShowTips()
	{
		ShowTips = showTips.isOn;
	}


	private class LocalizedLanguage
	{
		public string englishName;
		public string localisedName;
		public LocalizedLanguage(string englishName, string localisedName)
		{
			this.englishName = englishName;
			this.localisedName = localisedName;
		}
	}

	private string GetLocalizedLanguageName(string languageName)
	{
		return languageNames.First(x => x.englishName == languageName).localisedName;
	}

	private string GetEnglishLanguageName(string localizedName)
	{
		return languageNames.First(x => x.localisedName == localizedName).englishName;
	}

}

