using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Village
{
	public class Credits : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField]
		private Scrollbar creditsScroll;

		[SerializeField]
		private float speed;

		[SerializeField]
		private float delay;

		[SerializeField]
		private bool scroll;

		private void Start()
		{
			//Invoke("StartScroll", delay);
		}

		public void StartScroll()
		{
			scroll = true;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			//scroll = false;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			//scroll = false;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			//scroll = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			//scroll = true;
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
