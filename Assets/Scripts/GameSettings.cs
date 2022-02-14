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
		LoadLanguages();
	}

	public void LoadResolutions()
	{
		var resolutionStrings = Screen.resolutions.
			Select(x => $"{x.width}x{x.height}").ToList();
		resolution.ClearOptions();
		resolution.AddOptions(resolutionStrings);
	}

	public void LoadLanguages()
	{
		var langNames = LeanLocalization.CurrentLanguages.Keys.Select(x => new TMP_Dropdown.OptionData(x));
		var selectedLang = LeanLocalization.Instances.First().CurrentLanguage;
		language.options.AddRange(langNames);
		language.SetValueWithoutNotify(language.options.IndexOf(language.options.First(x => x.text == selectedLang)));
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

	public void SetLanguage()
	{
		string lang = language.options[language.value].text;
		LeanLocalization.SetCurrentLanguageAll(lang);
	}
}

