using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Village.Controllers;
using static Village.Controllers.GameController;

namespace Village.Views
{
	[SelectionBase]
	public class EventView : MonoBehaviour
	{
		[SerializeField]
		private GameEvent gameEvent;

		[SerializeField]
		private Color fontColor;

		public int startTurn;

		public GameEvent Event => gameEvent;

		public int TurnsLeft => startTurn + gameEvent.eventBase.turnDuration - instance.GetCurrentTurn();

		#region Unity referencess

		[SerializeField]
		private GameObject contentParent, requirementsList, successList, failureList;

		[SerializeField]
		private GameObject requirements, success, failure;

		[SerializeField]
		private TMP_Text titleText, descriptionText, turnText;

		[SerializeField]
		private EffectView effectViewPrefab;

		public void SetTurnLeft(int value)
		{
			turnText.text = value.ToString();
		}

		#endregion

		public void Load(GameEvent gameEvent)
		{
			this.gameEvent = gameEvent;
			startTurn = gameEvent.turn;

			titleText.text = gameEvent.eventBase.Title;
			descriptionText.text = gameEvent.eventBase.Description;

			requirements.SetActive(gameEvent.eventBase.requirements.Count > 0);
			foreach (var req in gameEvent.eventBase.requirements)
			{
				var reqView = Instantiate(effectViewPrefab, requirementsList.transform);
				int amount = Mathf.RoundToInt(req.Amount * instance.GetDifficultyMultiplier());
				reqView.SetAmount(amount);
				reqView.SetIcon(req.resource.icon);
				reqView.SetIconColor(req.resource.color);
				reqView.SetFontColor(fontColor);
			}

			success.SetActive(gameEvent.eventBase.onSuccess.Count > 0);
			foreach (var req in gameEvent.eventBase.onSuccess)
			{
				var reqView = Instantiate(effectViewPrefab, successList.transform);
				reqView.SetAmount(req.value);
				reqView.SetIcon(req.effect.icon);
				reqView.SetIconColor(req.effect.color);
				reqView.SetFontColor(fontColor);
			}

			failure.SetActive(gameEvent.eventBase.onFailure.Count > 0);
			foreach (var req in gameEvent.eventBase.onFailure)
			{
				var reqView = Instantiate(effectViewPrefab, failureList.transform);
				reqView.SetAmount(req.value);
				reqView.SetIcon(req.effect.icon);
				reqView.SetIconColor(req.effect.color);
				reqView.SetFontColor(fontColor);
			}
			RefreshData();
			RefreshLayout();
		}

		public void RefreshData()
		{
			turnText.text = TurnsLeft.ToString();
		}

		public void ChangeDetailsVisibility(bool show)
		{
			contentParent.SetActive(show);
		}

		public void ChangeDetailsVisibility()
		{
			contentParent.SetActive(!contentParent.activeSelf);
			RefreshLayout();
			EventSystem.current.SetSelectedGameObject(null);
		}

		private void RefreshLayout()
		{
			var refresher = GetComponentInParent<LayoutRefresher>();
			refresher.RefreshContentFitters();
			refresher.RefreshContentFitters();
		}
	}
}