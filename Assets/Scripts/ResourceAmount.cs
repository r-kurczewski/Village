using System;
using Village.Scriptables;

[Serializable]
public class ResourceAmount
{
	public Resource resource;
	public int amount;

	public ResourceAmount(Resource resource, int amount)
	{
		this.resource = resource;
		this.amount = amount;
	}
}