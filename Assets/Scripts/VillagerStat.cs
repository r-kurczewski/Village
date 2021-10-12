using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Stat", menuName ="Village/VillagerStat")]
public class VillagerStat : ScriptableObject
{
    public string statName;
    public Sprite statIcon;
    public Color statColor;
}
