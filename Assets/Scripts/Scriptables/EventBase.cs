﻿using Lean.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Village.Controllers;
using static Village.Scriptables.Effect;
using static Village.Scriptables.Resource;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "Event", menuName = "Village/Event")]
	public partial class EventBase : ScriptableObject
	{
		[SerializeField]
		private string localeTitle;

		[SerializeField]
		private string localeDescription;

		[SerializeField]
		private string localeLogSuccess;

		[SerializeField]
		private string localeLogFailure;

		public int turnDuration;

		public int eventPriority;

		public List<ResourceAmount> requirements;

		public List<EffectAmount> onSuccess;

		public List<EffectAmount> onFailure;

		public string Title => LeanLocalization.GetTranslationText(localeTitle);

		public string Description => LeanLocalization.GetTranslationText(localeDescription);

		public void ApplySuccess()
		{
			foreach (var eff in onSuccess)
			{
				eff.effect.Apply(eff.value);
			}
			GameController.instance.UpdateLogDayEntry(localeLogSuccess);
		}

		public void ApplyFailure()
		{
			foreach (var eff in onFailure)
			{
				eff.effect.Apply(eff.value);
			}
			GameController.instance.UpdateLogDayEntry(localeLogFailure);
		}
	}
}