using UnityEngine;
using UnityEngine.UI;

namespace Village.Views
{
	public class VillageStatView : MonoBehaviour
	{
		public Color color;
		public Color disabledColor;

		public void SetStat(int value)
		{
			int i = 0;
			foreach (Transform point in transform)
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
}