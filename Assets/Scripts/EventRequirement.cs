using System;

public class EventRequirement
{
	public delegate bool EventCondition(GameData data); 
	public string Description { get; set; }


}