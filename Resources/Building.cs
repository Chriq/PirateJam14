using Godot;
using System;

public partial class Building : Resource
{
	[Export] public int maxTurnsOfHealth = 3;
	[Export] public int buildCost;
	[Export] public int repairCost;
	[Export] public BuildingType type;
}

public enum ResourceType {
	WOOD,
	ELECTRICITY,
	STEEL
}

public enum BuildingType {
	// Static Defenses
	WALL,
	
	// Passive Defenses
	LANDMINE,
	
	// Active Defenses 
	FREEZERAY,
	LASER,

	// Resource Buildings
	LUMBER_MILL,
	POWER_PLANT
}

public enum BuildingState {
	ACTIVE,
	BUILDING,
	DESTROYED,
}
