using TMPro;
using UnityEngine;

[SelectionBase]
public class TextTooltip : Tooltip<string>
{
	public TMP_Text label;
	
	public override void Load(string text)
	{
		label.text = text;
	}

}