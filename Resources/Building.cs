using Godot;
using System;

public partial class Building : Resource
{
	[Export] public int maxTurnsOfHealth = 1;
	[Export] public int buildCost;
	[Export] public int repairCost;
	[Export] public BuildingType type;
}

public enum ResourceType {
	WOOD,
	ELECTRICITY
}

public enum BuildingType {
	WALL,
	LAZER,
	LANDMINE,

	LUMBER_MILL,
	POWER_PLANT
}