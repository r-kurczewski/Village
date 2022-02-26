using UnityEngine;
using UnityEngine.UI;
using Village.Scriptables;

namespace Village.Views
{
	public class VillageStatView : MonoBehaviour
	{
		[SerializeField]
		private VillagerStat stat;

		[SerializeField]
		private Sprite lockedIcon;

		[SerializeField]
		private Color disabledColor;

		private Color InactiveStatColor(Color color)
		{
			float statColorParam = 0.40f;
			float r = statColorParam * color.r + (1 - statColorParam) * disabledColor.r;
			float g = statColorParam * color.g + (1 - statColorParam) * disabledColor.g;
			float b = statColorParam * color.b + (1 - statColorParam) * disabledColor.b;
			return new Color(r,g,b);
		}


		public void SetStat(int effectiveStatValue, int statValue)
		{
			int i = 0;
			foreach (Transform point in transform)
			{
				var background = point.GetComponent<Image>();
				var image = point.transform.GetChild(0).GetComponent<Image>();
				if (i < effectiveStatValue)
				{
					image.sprite = stat.icon;
					background.color = stat.color;
				}
				else if (i < statValue)
				{
					image.sprite = stat.icon;
					background.color = InactiveStatColor(stat.color);
				}
				else
				{
					image.sprite = lockedIcon;
					background.color = disabledColor;
				}
				i++;
			}
		}
	}
}