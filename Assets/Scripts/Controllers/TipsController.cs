using Lean.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Village.Views;

namespace Village.Controllers
{
	public class TipsController : PromptWindow
	{
		public List<TurnHint> tipsToLoad;
		public List<string> displayedTips = new List<string>();

		public void TryLoadTip(string messageLocale)
		{
			if (TipDisplayed(messageLocale)) return;
			displayedTips.Add(messageLocale);

			if (!GameSettings.ShowTips) return;

			gameObject.SetActive(true);
			var hintMessage = LeanLocalization.GetTranslationText(messageLocale);
			LoadMessage(hintMessage);
			OnAccept.AddListener(() =>
			{
				gameObject.SetActive(false);
			});

			OnDecline.AddListener(() =>
			{
				GameSettings.ShowTips = false;
				gameObject.SetActive(false);
			});
		}

		public bool TipDisplayed(string messageLocale)
		{
			return displayedTips.Contains(messageLocale);
		}

		public void TipUpdate()
		{
			int turn = GameController.instance.GetCurrentTurn();
			foreach(var tip in tipsToLoad.Where(x=> x.turn == turn))
			{
				TryLoadTip(tip.hintLocale);
			}
		}

		[Serializable]
		public class TurnHint 
		{
			public int turn;
			[LeanTranslationName]
			public string hintLocale;

			public TurnHint(int turn, string hintLocale)
			{
				this.turn = turn;
				this.hintLocale = hintLocale;
			}
		}
	}
}
