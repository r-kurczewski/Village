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
		LoadMusic();
		LoadLanguages();
	}

	public void LoadResolutions()
	{
		resolution.ClearOptions();
		var resolutions = Screen.resolutions.Select(x => $"{x.width}x{x.height}").ToList();
		resolution.AddOptions(resolutions);
		var currentResolution = $"{Screen.width}x{Screen.height}";
		var currentResolutionIndex = resolution.options.IndexOf(resolution.options.FirstOrDefault(x=> x.text== currentResolution));
		resolution.SetValueWithoutNotify(currentResolutionIndex);
		fullscreen.isOn = Screen.fullScreen;
	}

	public void LoadLanguages()
	{
		var langNames = LeanLocalization.CurrentLanguages.Keys.Select(x => new TMP_Dropdown.OptionData(x));
		var selectedLang = LeanLocalization.Instances.First().CurrentLanguage;
		language.options.AddRange(langNames);
		language.SetValueWithoutNotify(language.options.IndexOf(language.options.First(x => x.text == selectedLang)));
	}

	public void LoadMusic()
	{
		music.value = AudioListener.volume;
	}

	public void ChangeResolution()
	{
		var res = resolution.options[resolution.value].text.Split('x');
		Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), fullscreen.isOn);
	}

	public void ChangeVolume()
	{
		AudioListener.volume = music.value;
	}

	public void SetLanguage()
	{
		string lang = language.options[language.value].text;
		LeanLocalization.SetCurrentLanguageAll(lang);
	}
}

