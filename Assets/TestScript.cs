using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Controllers;

public class TestScript : MonoBehaviour
{
	[SerializeField]
	private AudioClip effect;

	void Start()
	{
		InvokeRepeating("PlayEffect", 1, 20);

	}

	private void PlayEffect()
	{
		Debug.Log("Played");
		AudioController.instance.PlaySound(effect);

	}
}
