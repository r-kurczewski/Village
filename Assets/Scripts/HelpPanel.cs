using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
   public void ChangeVisibility()
	{
		gameObject.SetActive(!gameObject.activeSelf);
	}
}
