using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ResourcePanelView : MonoBehaviour
{
	[SerializeField]
	private List<ResourceView> resources;

	private void Start()
	{
		resources = GetComponentsInChildren<ResourceView>().ToList();
	}

	public void Refresh()
	{
		resources.ForEach(x => x.Refresh());
	}
}
