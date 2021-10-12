using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
	public Color color;
	public Color disabledColor;

	//private void OnEnable()
	//{
	//	SetStat(2);
	//}

	public void SetStat(int value)
	{
		int i = 0;
		foreach(Transform point in transform)
		{
			var background = point.GetComponent<Image>();
			if (i < value)
			{
				background.color = color;
			}
			else
			{
				background.color = disabledColor;
			}
			i++;
		}
	}
}