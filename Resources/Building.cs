using Godot;
using System;

public partial class Building : Resource
{
	[Export] public int maxTurnsOfHealth = 1;
	[Export] public int buildCost;
	[Export] public int repairCost;
}

public enum ResourceType {
	WOOD,
	ELECTRICITY,
	STEEL
}