using UnityEngine;
using UnityEngine.EventSystems;
using Village.Scriptables;

[SelectionBase]
public class Villager : MonoBehaviour
{
	private const int MaxHealth = 4;

	public VillagerBase villagerBase;
	public int strength, gathering, crafting, diplomacy, intelligence;
	
	[Range(0, 4)]
	public int health;

	public void Load(VillagerBase villagerBase)
	{
		this.villagerBase = villagerBase;
		strength = villagerBase.baseStrength;
		gathering = villagerBase.baseGathering;
		crafting = villagerBase.baseCrafting;
		diplomacy = villagerBase.baseDiplomacy;
		intelligence = villagerBase.baseIntelligence;
		health = Random.Range(1, MaxHealth + 1);
	}
}
