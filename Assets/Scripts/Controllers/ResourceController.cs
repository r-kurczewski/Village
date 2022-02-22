using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Village.Views;
using Village.Scriptables;
using System;
using static Village.Scriptables.Resource;
using static Village.Controllers.GameController;
using Random = UnityEngine.Random;
using static TradeOffer;

namespace Village.Controllers
{
	[SelectionBase]
	public class ResourceController : MonoBehaviour
	{
		[SerializeField]
		private List<ResourceView> views;

		[SerializeField]
		private List<ResourceAmount> resources;

		public void LoadResources()
		{
			foreach (var view in views)
			{
				resources.Add(new ResourceAmount(view.Resource, 0));
			}
		}

		public void LoadResources(List<ResourceAmount.SaveData> save)
		{
			for (int i = 0; i < views.Count; i++)
			{
				resources.Add(new ResourceAmount(views[i].Resource, save[i].amount));
			}
		}

		public void RefreshGUI()
		{
			foreach (var view in views)
			{
				int value = GetResourceAmount(view.Resource);
				view.SetAmount(value);
			}
		}

		public void AddRemoveResource(Resource resource, int amount)
		{
			resources.First(x => x.resource.ResourceName == resource.ResourceName).Amount += amount;
		}

		public int GetResourceAmount(Resource resource)
		{
			var a = resources;
			return resources.First(x => x.resource.ResourceName == resource.ResourceName).Amount;
		}

		public List<TradeOffer> GenerateTrades()
		{
			var res = resources
				.Select(x => x.resource)
				.Where(x => x.tradable)
				.OrderBy(x => Random.value);

			var sell = res
				.Take(MERCHANT_SELL_ITEMS_COUNT)
				.Select(x => new TradeOffer(x, TradeMode.Sell));

			var buy = res
				.Skip(MERCHANT_SELL_ITEMS_COUNT)
				.Take(MERCHANT_BUY_ITEMS_COUNT)
				.Select(x => new TradeOffer(x, TradeMode.Buy));

			return sell.Concat(buy).ToList();
		}

		public List<ResourceAmount.SaveData> SaveResources()
		{
			return resources.Select(x => x.Save()).ToList();
		}
	}
}
