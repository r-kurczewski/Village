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

	public int Strength => GetStatValue(references.strength);
	public int Gathering => GetStatValue(references.gathering);
	public int Crafting => GetStatValue(references.crafting);
	public int Diplomacy => GetStatValue(references.diplomacy);
	public int Intelligence => GetStatValue(references.intelligence);
	public int Health { get => _health; set => _health = Mathf.Clamp(value, 0, HEALTH_MAX); }

	public void Load(VillagerBase villagerBase)
	{
		this.villagerBase = villagerBase;
		SetStat(references.strength, villagerBase.baseStrength);
		SetStat(references.gathering, villagerBase.baseGathering);
		SetStat(references.crafting, villagerBase.baseCrafting);
		SetStat( references.diplomacy, villagerBase.baseDiplomacy);
		SetStat( references.intelligence, villagerBase.baseIntelligence);
		Health = HEALTH_MAX;
	}

	public int GetStatValue(VillagerStat stat)
	{
		return stats.First(x => x.stat == stat).Amount;
	}

	public void SetStat(VillagerStat stat, int value)
	{
		stats.First(x => x.stat == stat).Amount = value;
	}

	public void IncreaseDecreaseAllStats(int value)
	{
		stats.ForEach(x => x.Amount += value);
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
				amount = Mathf.Clamp(amount + value, 0, STAT_MAX);
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
