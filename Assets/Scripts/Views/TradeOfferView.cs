using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Village.Controllers;
using Village.Scriptables;

namespace Village.Views
{
	public class TradeOfferView : MonoBehaviour
	{
		[SerializeField]
		private ResourceView resource;

		[SerializeField]
		private ResourceView gold;

		[SerializeField]
		private TMP_Text tradeCountLabel;

		[SerializeField]
		private Button lessButton;

		[SerializeField]
		private Button moreButton;

		[SerializeField]
		private Resource goldReference;

		public int TradeCount { get; private set; }

		private TradeController controller;

		public TradeOffer Offer { get; private set; }

		public void Load(TradeOffer offer, TradeController controller, Villager villager)
		{
			Offer = offer;
			this.controller = controller;
			resource.Load(offer.resource);
			resource.SetAmount(1);
			gold.Reload();
			gold.SetAmount(offer.GetCost(villager));
			Refresh();
		}

		public void TradeMore()
		{

			TradeCount++;
			Refresh();
			controller.Refresh();
		}

		public void TradeLess()
		{
			TradeCount--;
			Refresh();
			controller.Refresh();
		}

		private void Refresh()
		{
			tradeCountLabel.text = TradeCount.ToString();
			if (TradeCount <= 0)
			{
				lessButton.interactable = false;
			}
			else
			{
				lessButton.interactable = true;
			}
		}
	}
}