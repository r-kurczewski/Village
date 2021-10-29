using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Village.Views;
using Village.Scriptables;
using System;
using static Village.Scriptables.Resource;

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
			views = GetComponentsInChildren<ResourceView>().ToList();
			foreach(var view in views)
			{
				view.Reload();
				resources.Add(new ResourceAmount(view.Resource, 0));
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
			resources.First(x => x.resource == resource).amount += amount;
		}

		public int GetResourceAmount(Resource resource)
		{
			return resources.First(x => x.resource == resource).amount;
		}
	}
}
