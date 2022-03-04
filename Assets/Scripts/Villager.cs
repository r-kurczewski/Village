using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Village.Scriptables;
using Random = UnityEngine.Random;
using static Village.Controllers.GameController;

[SelectionBase]
public class Villager : MonoBehaviour
{
	[SerializeReference]
	public VillagerBase villagerBase;

	[SerializeField]
	private int
		strength,
		gathering,
		crafting,
		diplomacy,
		intelligence;

	[SerializeField, Range(0, 4)]
	private int _health;

	[SerializeField]
	private UnityData references;

	#region Stats properties

	public int BaseStrength => villagerBase.baseStrength;
	public int BaseGathering => villagerBase.baseGathering;
	public int BaseCrafting => villagerBase.baseCrafting;
	public int BaseDiplomacy => villagerBase.baseDiplomacy;
	public int BaseIntelligence => villagerBase.baseIntelligence;

	public int Strength => strength;
	public int Gathering => gathering;
	public int Crafting => crafting;
	public int Diplomacy => diplomacy;
	public int Intelligence => intelligence;

	public int EffectiveStrength => GetEffectiveStatValue(references.strength);
	public int EffectiveGathering => GetEffectiveStatValue(references.gathering);
	public int EffectiveCrafting => GetEffectiveStatValue(references.crafting);
	public int EffectiveDiplomacy => GetEffectiveStatValue(references.diplomacy);
	public int EffectiveIntelligence => GetEffectiveStatValue(references.intelligence);

	#endregion

	public int Health { get => _health; set => _health = Mathf.Clamp(value, 0, HEALTH_MAX); }

	public void Load(VillagerBase vBase)
	{
		villagerBase = vBase;
		strength = vBase.baseStrength;
		gathering = vBase.baseGathering;
		crafting = vBase.baseCrafting;
		diplomacy = vBase.baseDiplomacy;
		intelligence = vBase.baseIntelligence;
		Health = HEALTH_MAX;
	}

	public void Load(SaveData data, VillagerBase vBase)
	{
		villagerBase = vBase;
		strength = data.strength;
		gathering = data.gathering;
		crafting = data.crafting;
		diplomacy = data.diplomacy;
		intelligence = data.intelligence;
		Health = data.health;
	}

	private ref int GetStatReference(VillagerStat statRef)
	{
		if (statRef == references.strength)
		{
			return ref strength;
		}
		else if (statRef == references.gathering)
		{
			return ref gathering;
		}
		else if (statRef == references.crafting)
		{
			return ref crafting;
		}
		else if (statRef == references.diplomacy)
		{
			return ref diplomacy;
		}
		else if (statRef == references.intelligence)
		{
			return ref intelligence;
		}
		else
		{
			throw new InvalidOperationException("Invalid villager stat");
		}
	}

	private int GetBaseStat(VillagerStat statRef)
	{
		if (statRef == references.strength)
		{
			return villagerBase.baseStrength;
		}
		else if (statRef == references.gathering)
		{
			return villagerBase.baseGathering;
		}
		else if (statRef == references.crafting)
		{
			return villagerBase.baseCrafting;
		}
		else if (statRef == references.diplomacy)
		{
			return villagerBase.baseDiplomacy;
		}
		else if (statRef == references.intelligence)
		{
			return villagerBase.baseIntelligence;
		}
		else
		{
			throw new InvalidOperationException("Invalid villager stat");
		}
	}

	public int GetStat(VillagerStat statRef)
	{
		return GetStatReference(statRef);
	}

	private void SetStat(VillagerStat stat, int value)
	{
		GetStatReference(stat) = value;
	}

	public int GetEffectiveStatValue(VillagerStat stat)
	{
		return Mathf.Clamp(GetStat(stat) + GetHealthStatModfier(), 0, STAT_MAX);
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

	public void ApplyIntelligenceBonus()
	{
		for (int i = 0; i < intelligence; i++)
		{
			int boostedStat = Random.Range(0, 4);
			switch (boostedStat)
			{
				case 0:
					strength++;
					break;

				case 1:
					gathering++;
					break;

				case 2:
					crafting++;
					break;

				case 3:
					diplomacy++;
					break;

				default:
					Debug.LogError("Invalid number");
					break;
			}
		}
	}

	public SaveData Save()
	{
		var data = new SaveData();
		data.villagerName = villagerBase.name;
		data.strength = strength;
		data.gathering = gathering;
		data.crafting = crafting;
		data.diplomacy = diplomacy;
		data.intelligence = intelligence;
		data.health = Health;
		return data;
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

	[Serializable]
	public class SaveData
	{
		public string villagerName;
		public int
			health,
			strength,
			gathering,
			crafting,
			diplomacy,
			intelligence;


	}
}
