using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Village.Scriptables;
using Village.Views;
using static TradeOffer.TradeMode;
using static Village.Controllers.GameController;

namespace Village.Controllers
{
	[SelectionBase]
	public class TradeController : MonoBehaviour
	{
		[SerializeField]
		private TradeOfferView offerPrefab;

		[SerializeField]
		private Transform sellList, buyList;

		[SerializeField]
		private Resource gold;

		[SerializeField]
		private TMP_Text goldLabel, discountLabel;

		[SerializeField]
		private Image villagerIcon;

		[SerializeField]
		private Button acceptButton;

		[SerializeField]
		private List<TradeOffer> merchantTrades;

		private Villager villager;

		private List<TradeOfferView> offerViews = new List<TradeOfferView>();

		public void Load(List<TradeOffer> offers, Villager villager)
		{
			this.villager = villager;
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
				offerView.Load(offer, this, villager);
				offerViews.Add(offerView);

				goldLabel.text = 0.ToString();
				discountLabel.text = $"{GetTradeMultiplier(villager)}%";
				villagerIcon.sprite = villager.villagerBase.avatar;
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

		private int GetTradeMultiplier(Villager villager)
		{
			return Mathf.RoundToInt(villager.Diplomacy * TRADE_DISCOUNT * 100);
		}

		public void ShowTradeWindow()
		{
			gameObject.SetActive(true);
		}

		public void HideTradeWindow()
		{
			gameObject.SetActive(false);
		}
		public void LoadTradeWindow(Villager villager)
		{
			Load(merchantTrades, villager);
		}

		public void Refresh()
		{
			goldLabel.text = GetTotalCost().ToString();
			acceptButton.interactable = IsOfferCorrect();
		}

		public bool IsOfferCorrect()
		{
			return instance.GetResourceAmount(gold) + GetTotalCost() >= 0;
		}

		public void FinishTrade()
		{
			foreach (var view in offerViews)
			{
				int cost = view.TradeCount * view.Offer.GetCost(villager);
				if (view.Offer.mode is Sell)
				{
					instance.AddRemoveResource(gold, cost);
					instance.AddRemoveResource(view.Offer.resource, -view.TradeCount);
				}
				else
				{
					instance.AddRemoveResource(gold, -cost);
					instance.AddRemoveResource(view.Offer.resource, view.TradeCount);
				}
				HideTradeWindow();
			}
		}

		public int GetTotalCost()
		{
			int gold = 0;
			foreach (var view in offerViews)
			{
				int offerCost = view.TradeCount * view.Offer.GetCost(villager);
				if (view.Offer.mode is Buy)
				{
					gold -= offerCost;
				}
				else if (view.Offer.mode is Sell)
				{
					gold += offerCost;
				}
			}
			return gold;
		}
	}
}