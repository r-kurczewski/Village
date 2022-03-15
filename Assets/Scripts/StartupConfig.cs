using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Encoders;

namespace Village
{
    public class StartupConfig : MonoBehaviour
    {
        static bool completed = false;

        [SerializeField]
        private bool saveEncode;

        private void Awake()
		{
            if (!completed)
            {
                SaveGame.Encode = saveEncode;
				QualitySettings.vSyncCount = 0;
				Application.targetFrameRate = 60;
				completed = true;
            }
		}
    }
}
