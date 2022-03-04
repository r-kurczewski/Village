using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Village
{
	public class Credits : MonoBehaviour
	{
		[SerializeField]
		private Scrollbar creditsScroll;

		[SerializeField]
		private float speed;

		[SerializeField]
		private bool scroll;

		public void StartScroll()
		{
			scroll = true;
		}

		private void Update()
		{
			if (scroll && creditsScroll.value >= 0)
			{
				creditsScroll.value -= speed * Time.deltaTime;
			}
		}
	}
}
