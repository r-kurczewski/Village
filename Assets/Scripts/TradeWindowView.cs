using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Village.Scriptables;
using Village.Views;
using static TradeOffer.TradeMode;

[SelectionBase]
public class TradeWindowView : MonoBehaviour
{
	[SerializeField]
	private TradeOfferView offerPrefab;

	[SerializeField]
	private Transform sellList, buyList;

	[SerializeField]
	private Resource gold;

	[SerializeField]
	private TMP_Text goldLabel;

	[SerializeField]
	private Button acceptButton;

	private List<TradeOfferView> offerViews;

	public void Load(List<TradeOffer> offers)
	{
		EmptyList();
		foreach (var offer in offers)
		{
			Transform parentList = null;
			switch (offer.mode)
			{
				case Buy:
					parentList = buyList;
					break;

				case Sell:
					parentList = sellList;
					break;
			}
			var offerView = Instantiate(offerPrefab, parentList);
			offerView.Load(offer, this);
			offerViews.Add(offerView);
		}
	}

	private void EmptyList()
	{
		foreach (var offerView in offerViews)
		{
			Destroy(offerView.gameObject);
		}
		offerViews.Clear();
	}

	public void Refresh()
	{
		goldLabel.text = GetTotalGold().ToString();
		acceptButton.interactable = IsOfferCorrect();
	}

	public bool IsOfferCorrect()
	{
		throw new System.NotImplementedException();
	}

	public int GetTotalGold()
	{
		int gold = 0;
		foreach (var view in offerViews)
		{
			if (view.Offer.merchantResources.resource == this.gold)
			{
				int offerCost = view.TradeCount * view.Offer.merchantResources.amount;
				if(view.Offer.mode is Buy)
				{
					gold -= offerCost;
				}
				else if (view.Offer.mode is Sell)
				{
					gold += offerCost;
				}
			}
		}
		return gold;
	}
}
