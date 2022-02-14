using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Lean.Localization;

namespace Village.Views
{
	public class TurnView : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text turnLabel;

		[SerializeField]
		private TMP_Text seasonLabel;

		public void SetTurn(int turn)
		{
			LeanLocalization.SetToken("turn", turn.ToString());
		}

		public void SetChapterName(string name)
		{
			LeanLocalization.SetToken("chapter", name);
		}
	}
}