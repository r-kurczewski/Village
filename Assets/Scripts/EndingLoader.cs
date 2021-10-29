using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.Scriptables;
using Village.Views;

public class EndingLoader : MonoBehaviour
{
	public static Message ending;

	[SerializeField]
	private MessageView messageView;

	private void Start()
	{
		messageView.LoadMessage(ending);
	}
}
