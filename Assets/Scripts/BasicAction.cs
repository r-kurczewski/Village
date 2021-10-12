using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Action", menuName = "Village/Action/BasicAction")]
public class BasicAction : Action
{
    [Header("Basic Effects")]
	public int heal;
	public int gold;
	public int food;
	public int water;
	public int medicine;
	public int wood;
	public int stone;
	public int material;
	public int herbs;
	public int metal;
	public int gems;
	public int countryAReputation;
	public int countryBReputation;

	public override void Apply(Villager target)
	{
		throw new System.NotImplementedException();
	}
}
