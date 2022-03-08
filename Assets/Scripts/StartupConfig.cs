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
        static bool finished = false;

        [SerializeField]
        private bool saveEncode;

        private void Awake()
		{
            if (!finished)
            {
                SaveGame.Encode = saveEncode;
                //QualitySettings.vSyncCount = 0;
                //Application.targetFrameRate = 60;
                finished = true;
            }
		}
    }
}
