using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Village.Views
{
	public class EffectView : MonoBehaviour
	{
		[SerializeField]
		private Image icon;

		[SerializeField]
		private TMP_Text amountText;

		public void SetIcon(Sprite sprite)
		{
			icon.sprite = sprite;
		}

		public void SetAmount(int value)
		{
			amountText.text = value.ToString();
		}

		public void SetIconColor(Color color)
		{
			icon.color = color;
		}

		public void SetFontColor(Color color)
		{
			amountText.color = color;
		}

	}
}