using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Village/Resource")]
public class Resource : ScriptableObject
{
	public string resourceName;
	public Sprite icon;
	public int baseValue;
}