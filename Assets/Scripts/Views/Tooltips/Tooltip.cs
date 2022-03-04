using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.Views.Tooltips
{

	public abstract class Tooltip<T> : MonoBehaviour
	{
		public static Tooltip<T> instance;

		private void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(instance.gameObject);
			}
			instance = this;
			gameObject.SetActive(false);
		}

		public abstract void Load(T data);
	}
}
