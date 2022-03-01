using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Village
{
	[ExecuteInEditMode]
	public class VersionLoader : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text text;

		[SerializeField]
		private string versionPrefix;

		void Start()
		{
			text.text = versionPrefix + Application.version;
		}
	}
}
