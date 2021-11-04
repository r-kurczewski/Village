using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Village.Controllers;
using Village.Scriptables;
using Random = UnityEngine.Random;
using static Village.Controllers.GameController;

[SelectionBase]
public class Villager : MonoBehaviour
{
	public VillagerBase villagerBase;

	[SerializeField]
	private List<VillagerStatAmount> stats;

	[SerializeField, Range(0, 4)]
	private int _health;

	[SerializeField]
	private UnityData references;

	public int BaseStrength => GetBaseStatValue(references.strength);
	public int BaseGathering => GetBaseStatValue(references.gathering);
	public int BaseCrafting => GetBaseStatValue(references.crafting);
	public int BaseDiplomacy => GetBaseStatValue(references.diplomacy);
	public int BaseIntelligence => GetBaseStatValue(references.intelligence);
	public int Strength => GetEffectiveStatValue(references.strength);
	public int Gathering => GetEffectiveStatValue(references.gathering);
	public int Crafting => GetEffectiveStatValue(references.crafting);
	public int Diplomacy => GetEffectiveStatValue(references.diplomacy);
	public int Intelligence => GetEffectiveStatValue(references.intelligence);

	public int Health { get => _health; set => _health = Mathf.Clamp(value, 0, HEALTH_MAX); }

	public void Load(VillagerBase villagerBase)
	{
		this.villagerBase = villagerBase;
		SetStat(references.strength, villagerBase.baseStrength);
		SetStat(references.gathering, villagerBase.baseGathering);
		SetStat(references.crafting, villagerBase.baseCrafting);
		SetStat(references.diplomacy, villagerBase.baseDiplomacy);
		SetStat(references.intelligence, villagerBase.baseIntelligence);
		Health = HEALTH_MAX;
	}

	private VillagerStatAmount GetStat(VillagerStat stat)
	{
		return stats.First(x => x.stat == stat);
	}

	public int GetBaseStatValue(VillagerStat stat)
	{
		return GetStat(stat).Amount;
	}

	public int GetEffectiveStatValue(VillagerStat stat)
	{
		return Mathf.Clamp(GetBaseStatValue(stat) + GetHealthStatModfier(), 0, STAT_MAX);
	}

	private int GetHealthStatModfier()
	{
		switch (Health)
		{
			case 4:
				return 0;

			case 3:
				return -1;

			case 2:
				return -2;

			case 1:
				return -4;

			default:
				Debug.LogWarning("Health error: " + Health);
				return -4;
		}
	}

	public void SetStat(VillagerStat stat, int value)
	{
		GetStat(stat).Amount = value;
	}

	public void ApplyIntelligenceBonus()
	{
		var boostableStats = new List<VillagerStatAmount>(stats);
		boostableStats.Remove(GetStat(references.intelligence));
		int intelligence = BaseIntelligence;
		for (int i = 0; i < intelligence; i++)
		{
			boostableStats[Random.Range(0, boostableStats.Count)].Amount++;
		}
	}

	[Serializable]
	public class VillagerStatAmount
	{
		public VillagerStat stat;
		[SerializeField]
		private int amount;

		public int Amount
		{
			get
			{
				return amount;
			}

			set
			{
				amount = Mathf.Clamp(value, 0, STAT_MAX);
			}
		}

		public VillagerStatAmount() { }

		public VillagerStatAmount(VillagerStat stat, int amount)
		{
			this.stat = stat;
			this.amount = amount;
		}
	}

	[Serializable]
	private class UnityData
	{
		public VillagerStat
			strength,
			gathering,
			crafting,
			diplomacy,
			intelligence;

	}
}
