using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReputationBar : MonoBehaviour
{
    public Slider negative;
    public Color negativeColor;
    public Slider positive;
    public Color positiveColor;
    public TMP_Text label;
	public Color neutralColor;

	public string LabelString(int value, Color color) => $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{value}</color>";

	public void SetReputation(int value)
	{
        if(value < 0)
		{
			negative.value = Mathf.Abs(value);
			positive.value = 0;
			label.text = LabelString(Mathf.Abs(value), negativeColor);
		}
        else if(value == 0)
		{
			negative.value = 0;
			positive.value = 0;
			label.text = LabelString(value, neutralColor);
		}
		else
		{
			negative.value = 0;
			positive.value = value;
			label.text = LabelString(value, positiveColor);
		}
	}
}
