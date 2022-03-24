using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Village.Controllers;
using static Village.Controllers.GameController;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "Event", menuName = "Village/EnvoyEvent")]
	public class EnvoyEventBase : EventBase
	{
		private const string envoyTipLocale = "tips/envoy";

		public override void OnLoad(bool saveLoad)
		{
			instance.TryLoadHint(envoyTipLocale);
		}
	}
}
