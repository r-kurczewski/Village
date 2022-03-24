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
	[CreateAssetMenu(fileName = "Event", menuName = "Village/MerchantEvent")]
	public class MerchantEventBase : EventBase
	{
		private const string merchantTipLocale = "tips/merchant";
		public override void OnLoad(bool saveLoad)
		{
			if (!saveLoad)
			{
				instance.LoadNewMerchantTrades();
			}
			instance.TryLoadHint(merchantTipLocale);
		}
	}
}
