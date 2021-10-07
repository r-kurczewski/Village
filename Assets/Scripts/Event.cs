using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class Event : MonoBehaviour
{
	public string Title { get; set; }
	public string Description { get; set; }
	public List<EventRequirement> Requirements { get; set; }
	public List<EventEffect> OnSuccess { get; set; }
	public List<EventEffect> OnFailure { get; set; }
	public int Turns { get; set; }

	#region Unity references

	[SerializeField]
	private GameObject info;

	[SerializeField]
	private TMP_Text titleText, descriptionText, requirementsText, onSuccessText, onFailureText;

	#endregion

	private void Start()
	{

	}

	public void TurnUpdate()
	{

	}

	public void RequirementsUpdate()
	{

	}

	public void ChangeInfoVisibility(bool show)
	{
		info.SetActive(show);
	}

	public void ChangeInfoVisibility()
	{
		info.SetActive(!info.activeSelf);
		RefreshLayout();
	}

	private void RefreshLayout()
	{
		GetComponentInParent<LayoutRefresher>().RefreshContentFitters();
	}
}
