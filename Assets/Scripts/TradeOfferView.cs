using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeOfferView : MonoBehaviour
{
	[SerializeField]
    private ResourceView givenResource;

	[SerializeField]
    private ResourceView takenResource;

	[SerializeField]
	private TMP_Text tradeCountLabel;

	[SerializeField]
	private Button lessButton;

	[SerializeField]
	private Button moreButton;

	public int TradeCount { get; private set; }

	private TradeWindow parentWindow;

	public TradeOffer Offer { get; private set; }

	public void Load(TradeOffer offer, TradeWindow parentWindow)
	{
		Offer = offer;
		this.parentWindow = parentWindow;
		givenResource.SetResource(offer.yourResources.resource);
		givenResource.SetAmount(offer.yourResources.amount);
		takenResource.SetResource(offer.merchantResources.resource);
		takenResource.SetAmount(offer.merchantResources.amount);
		Refresh();
	}

	public void TradeMore()
	{

		TradeCount++;
		Refresh();
		parentWindow.Refresh();
	}

    public void TradeLess()
	{
		TradeCount--;
		Refresh();
		parentWindow.Refresh();
	}

	private void Refresh()
	{
		tradeCountLabel.text = TradeCount.ToString();
		if(TradeCount <= 0)
		{
			lessButton.interactable = false;
		}
		else
		{
			lessButton.interactable = true;
		}
	}
}
