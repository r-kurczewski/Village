using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Lean.Localization;

namespace Village.Views
{
	public class TurnView : MonoBehaviour
	{
		public void SetTurn(int turn)
		{
			LeanLocalization.SetToken("turn", turn.ToString());
			LeanLocalization.UpdateTranslations();
		}

		public void SetChapterName(string name)
		{
			LeanLocalization.SetToken("chapter", name);
			LeanLocalization.UpdateTranslations();
		}
	}
}