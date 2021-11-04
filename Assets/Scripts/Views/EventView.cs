using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Village.Controllers.GameController;

namespace Village.Views
{
	[SelectionBase]
	public class EventView : MonoBehaviour
	{
		[SerializeField]
		private GameEvent gameEvent;

		public int turnsLeft;

		public GameEvent Event => gameEvent;

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

			turnsLeft = gameEvent.eventBase.turnDuration + instance.GetPredictionFactor();

			titleText.text = gameEvent.eventBase.title;
			descriptionText.text = gameEvent.eventBase.description;

			requirements.SetActive(gameEvent.eventBase.requirements.Count > 0);
			foreach (var req in gameEvent.eventBase.requirements)
			{
				var reqView = Instantiate(effectViewPrefab, requirementsList.transform);
				reqView.SetAmount(req.Amount);
				reqView.SetIcon(req.resource.icon);
				reqView.SetIconColor(req.resource.color);
				reqView.SetFontColor(Color.black);
			}

			success.SetActive(gameEvent.eventBase.onSuccess.Count > 0);
			foreach (var req in gameEvent.eventBase.onSuccess)
			{
				var reqView = Instantiate(effectViewPrefab, successList.transform);
				reqView.SetAmount(req.value);
				reqView.SetIcon(req.effect.icon);
				reqView.SetIconColor(req.effect.color);
				reqView.SetFontColor(Color.black);
			}

			failure.SetActive(gameEvent.eventBase.onFailure.Count > 0);
			foreach (var req in gameEvent.eventBase.onFailure)
			{
				var reqView = Instantiate(effectViewPrefab, failureList.transform);
				reqView.SetAmount(req.value);
				reqView.SetIcon(req.effect.icon);
				reqView.SetIconColor(req.effect.color);
				reqView.SetFontColor(Color.black);
			}
			RefreshData();
			RefreshLayout();
		}

		public void RefreshData()
		{
			turnText.text = turnsLeft.ToString();
		}

		public void ChangeDetailsVisibility(bool show)
		{
			contentParent.SetActive(show);
		}

		public void ChangeDetailsVisibility()
		{
			contentParent.SetActive(!contentParent.activeSelf);
			RefreshLayout();
		}

		private void RefreshLayout()
		{
			var refresher = GetComponentInParent<LayoutRefresher>();
			refresher.RefreshContentFitters();
			refresher.RefreshContentFitters();
		}
	}
}