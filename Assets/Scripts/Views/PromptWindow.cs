using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PromptWindow : MonoBehaviour
{
    public UnityEvent OnAccept;

    public UnityEvent OnDecline;

    [SerializeField]
    private TMP_Text label;

    public void SetMessage(string text)
	{
        label.text = text;
	}

    public void Close()
	{
        Destroy(gameObject);
	}

    public void Accept()
	{
        OnAccept.Invoke();
	}

    public void Decline()
	{
        OnDecline.Invoke();
	}
}
