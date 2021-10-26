using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
			turnLabel.text = $"Tura {turn}";
		}

		public void SetChapterName(string name)
		{
			seasonLabel.text = name;
		}
	}
}