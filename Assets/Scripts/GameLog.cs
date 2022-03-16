using Lean.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Village.Views;
using static Village.Controllers.GameController;

namespace Village
{
	public class GameLog : HidePanel
	{
		private const string entryTitleLocale = "log/entryTitle";

		[SerializeField]
		private TMP_Text logLabel;

		[SerializeField]
		private LayoutRefresher refresher;

		[SerializeField]
		private Scrollbar logScrollbar;

		[SerializeField]
		private ScrollRect scrollRect;

		[SerializeField]
		private List<LogEntry> log = new List<LogEntry>();

		[SerializeField]
		private List<string> dayBuffer = new List<string>();

		private string EntryTitle(int turn) => $"<b>{LeanLocalization.GetTranslationText(entryTitleLocale)} {turn}:</b>\n";

		public void UpdateDayEntry(string entryLocale)
		{
			if (entryLocale != string.Empty)
			{
				dayBuffer.Add(entryLocale);
			}
		}

		public void PrintDayEntry()
		{
			if (dayBuffer.Count > 0)
			{
				var turn = instance.GetCurrentTurn();
				var entry = new LogEntry(turn, new List<string>(dayBuffer));
				PrintDayEntry(entry);
				log.Add(entry);
			}
		}

		public void PrintDayEntry(LogEntry entry)
		{
			var entryMessage = entry.localeEntries.Select(x => $"~ {LeanLocalization.GetTranslationText(x)}");
			logLabel.text += EntryTitle(entry.turn);
			logLabel.text += string.Join("\n", entryMessage);
			logLabel.text += "\n\n";

			dayBuffer.Clear();
			refresher.RefreshContentFitters();
			ScrollToBottom();
		}

		public override void ChangeVisibility()
		{
			base.ChangeVisibility();
			if (gameObject.activeInHierarchy)
			{
				ScrollToBottom();
			}
		}

		public List<LogEntry> GetLogData()
		{
			return log;
		}

		public void SetLogData(List<LogEntry> logData)
		{
			log = logData;
			ReloadLog();
			ScrollToBottom();
		}

		private void ReloadLog()
		{
			logLabel.text = string.Empty;
			foreach (var entry in log)
			{
				PrintDayEntry(entry);
			}
		}

		private void ScrollToBottom()
		{
			scrollRect.verticalNormalizedPosition = 0;
		}

		[Serializable]
		public class LogEntry
		{
			public int turn;
			public List<string> localeEntries;

			public LogEntry(int turn, List<string> localeEntries)
			{
				this.turn = turn;
				this.localeEntries = localeEntries;
			}
		}
	}
}
