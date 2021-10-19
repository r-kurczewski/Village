using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class EventView : MonoBehaviour
{
	[SerializeField]
	private Event baseEvent;

	public int turnDuration;

	#region Unity referencess

	[SerializeField]
	private GameObject contentParent, requirementsList, successList, failureList;

	[SerializeField]
	private GameObject requirements, success, failure;

	[SerializeField]
	private TMP_Text titleText, descriptionText, turnText;

	[SerializeField]
	private EffectView effectViewPrefab;

	#endregion

	private void Start()
	{
		if(baseEvent) Load(baseEvent);
	}

	public void Load(Event ev)
	{
		baseEvent = ev;
		titleText.text = ev.title;
		descriptionText.text = ev.description;
		turnDuration = ev.turnDuration;

		turnText.text = turnDuration.ToString();

		requirements.SetActive(ev.requirements.Count > 0);
		foreach (var req in ev.requirements)
		{
			var reqView = Instantiate(effectViewPrefab, requirementsList.transform);
			reqView.SetAmount(req.amount);
			reqView.SetIcon(req.resource.icon);
			reqView.SetIconColor(req.resource.color);
			reqView.SetFontColor(Color.black);
		}

		success.SetActive(ev.onSuccess.Count > 0);
		foreach (var req in ev.onSuccess)
		{
			var reqView = Instantiate(effectViewPrefab, successList.transform);
			reqView.SetAmount(req.amount);
			reqView.SetIcon(req.effect.icon);
			reqView.SetIconColor(req.effect.color);
			reqView.SetFontColor(Color.black);
		}

		failure.SetActive(ev.onFailure.Count > 0);
		foreach (var req in ev.onFailure)
		{
			var reqView = Instantiate(effectViewPrefab, failureList.transform);
			reqView.SetAmount(req.amount);
			reqView.SetIcon(req.effect.icon);
			reqView.SetIconColor(req.effect.color);
			reqView.SetFontColor(Color.black);
		}
	}

	public void TurnUpdate()
	{
		turnDuration--;
		turnText.text = turnDuration.ToString();
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
		GetComponentInParent<LayoutRefresher>().RefreshContentFitters();
	}
}
