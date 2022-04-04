using Lean.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Village.Views;
using static Village.Controllers.GameController;

namespace Village.Controllers
{
	public class LogController : HidePanel
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
		private List<LogSubEntry> dayEntries = new List<LogSubEntry>();

		public void UpdateDayEntry(LogSubEntry subEntry)
		{
			if (subEntry.localeMessage != string.Empty)
			{
				dayEntries.Add(subEntry);
			}
		}

		public void PrintDayEntry()
		{
			if (dayEntries.Count > 0)
			{
				var turn = instance.GetCurrentTurn();
				var entry = new LogEntry(turn, new List<LogSubEntry>(dayEntries));
				PrintDayEntry(entry);
				log.Add(entry);
			}
		}

		public void PrintDayEntry(LogEntry entry)
		{
			logLabel.text += entry.FormatedMessage;
			dayEntries.Clear();
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
			public List<LogSubEntry> subEntries;

			public LogEntry(int turn, List<LogSubEntry> subEntries)
			{
				this.turn = turn;
				this.subEntries = subEntries;
			}

			private string EntryTitle(int turn) => $"<b>{LeanLocalization.GetTranslationText(entryTitleLocale)} {turn}:</b>\n";

			public string FormatedMessage
			{
				get
				{
					var result = EntryTitle(turn);
					result += string.Join("\n", subEntries.Select(x => x.FormatedMessage));
					result += "\n\n";
					return result;
				}
			}
		}

		[Serializable]
		public class LogSubEntry
		{
			public string localeMessage;
			public Dictionary<string, string> parameters;

			public LogSubEntry(string localeMessage)
			{
				this.localeMessage = localeMessage;
				parameters = new Dictionary<string, string>();
			}

			public void AddParameter(string key, string value)
			{
				parameters.Add(key, value);
			}

			public string FormatedMessage
			{
				get
				{
					var result = $"~ {LeanLocalization.GetTranslationText(localeMessage)}";
					foreach(var param in parameters)
					{
						result = result.Replace(param.Key, param.Value);
					}
					return result;
				}
			}
		}
	}
}
