using Lean.Localization;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Village.Controllers;

public class GameSettings : MonoBehaviour
{
	public const string languageString = "language";
	public const string masterVolumeString = "masterVolume";
	public const string musicVolumeString = "musicVolume";
	public const string effectsVolumeString = "effectsVolume";
	public const string resolutionString = "resolution";
	public const string fullscreenString = "fullscreen";
	public const string hideTooltipsString = "hideAdvancedTooltips";
	public const string revertVIllagersString = "revertVillagers";

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

	[SerializeField]
	private Toggle revertVillagers;

	private List<LocalizedLanguage> languageNames = new List<LocalizedLanguage>()
	{
		new LocalizedLanguage("Polish", "Polski"),
		new LocalizedLanguage("English", "English"),
	};

	public static bool FullScreen => PlayerPrefs.GetInt(fullscreenString, defaultValue: Screen.fullScreen ? 1 : 0) != 0;

	public static float MasterVolume => PlayerPrefs.GetFloat(masterVolumeString, defaultValue: 1);

	public static float MusicVolume => PlayerPrefs.GetFloat(musicVolumeString, defaultValue: 1);

	public static float EffectsVolume => PlayerPrefs.GetFloat(effectsVolumeString, defaultValue: 1);

	public static bool SimplifiedTooltips => PlayerPrefs.GetInt(hideTooltipsString, defaultValue: 0) != 0;

	public static bool RevertVillagers => PlayerPrefs.GetInt(revertVIllagersString, defaultValue: 1) != 0;

	private void Start()
	{
		LoadResolutions();
		LoadLanguages();
		LoadHideTooltips();
		LoadRevertVillagers();

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
		var currentResolution = PlayerPrefs.GetString("resolution");
		var currentResolutionIndex = resolution.options.IndexOf(resolution.options.FirstOrDefault(x => x.text == currentResolution));
		resolution.SetValueWithoutNotify(currentResolutionIndex);
		fullscreen.isOn = FullScreen;
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

	private void LoadHideTooltips()
	{
		hideTooltips.isOn = SimplifiedTooltips;
	}

	private void LoadRevertVillagers()
	{
		revertVillagers.isOn = RevertVillagers;
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
		LeanLocalization.SetCurrentLanguageAll(GetEnglishLanguageName(lang));
	}

	public void ChangeAdvancedTooltips()
	{
		PlayerPrefs.SetInt(hideTooltipsString, hideTooltips.isOn ? 1 : 0);
	}

	public void ChangeRevertVillagers()
	{
		PlayerPrefs.SetInt(revertVIllagersString, revertVillagers.isOn ? 1 : 0);
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

