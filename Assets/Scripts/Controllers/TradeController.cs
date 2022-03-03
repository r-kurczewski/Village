using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		private Image raycastBlock;

		[SerializeField]
		private bool _tradeActive;

		[SerializeField]
		private List<TradeOffer> merchantTrades;

		public bool TradeActive { get => _tradeActive; private set { _tradeActive = value; } }

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
			return Mathf.RoundToInt(villager.EffectiveDiplomacy * TRADE_DISCOUNT * 100);
		}

		public void LoadTrades(List<TradeOffer> trades)
		{
			merchantTrades = trades;
		}

		public void LoadTrades(List<TradeOffer.SaveData> save, Dictionary<string, ScriptableObject> assets)
		{
			merchantTrades = new List<TradeOffer>();
			foreach (var trade in save)
			{
				Resource resource = assets[trade.resourceName] as Resource;
				merchantTrades.Add(new TradeOffer(resource, trade.tradeMode));
			}
			
		}

		public void ShowTradeWindow()
		{
			gameObject.SetActive(true);
			raycastBlock.enabled = true;
			TradeActive = true;
		}

		public void HideTradeWindow()
		{
			gameObject.SetActive(false);
			raycastBlock.enabled = false;
			TradeActive = false;
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
			bool correctGold = instance.GetResourceAmount(gold) + GetTotalCost() >= 0;
			bool correctResources = true;
			foreach(var view in offerViews.Where(x=> x.Offer.mode == Sell))
			{
				if (instance.GetResourceAmount(view.Offer.resource) < view.TradeCount)
				{
					correctResources = false;
				}
			}
			return correctGold && correctResources;
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
				instance.UpdateGUI();
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

		public List<TradeOffer.SaveData> SaveTrades()
		{
			return merchantTrades.Select(x => x.Save()).ToList();
		}
	}
}