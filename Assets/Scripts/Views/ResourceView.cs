using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Village.Controllers;
using Village.Scriptables;
using Village.Views.Tooltips;

namespace Village.Views
{
	[SelectionBase]
	public class ResourceView : Tooltiped
	{
		[SerializeField]
		private TMP_Text label;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private Resource resource;

		public Resource Resource => resource;

		public void Load(Resource res)
		{
			resource = res;
			icon.sprite = res.icon;
			icon.color = res.color;
		}

		public void Reload()
		{
			Load(resource);
		}

		public void SetAmount(int value)
		{
			label.text = value.ToString();
		}

		protected override void LoadTooltipData()
		{
			var bonus = GameController.instance.GetTurnBonus(resource);
			string tooltip = resource.ResourceName;
			if (bonus > 0) tooltip += $" (+{bonus})";
			TextTooltip.instance.Load(tooltip);
		}

		protected override void SetTooltipObject()
		{
			tooltip = TextTooltip.instance.gameObject;
		}
	}
}