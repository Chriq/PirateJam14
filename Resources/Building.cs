using Godot;
using Godot.Collections;

public partial class Building : Resource
{
	[Export] public int maxTurnsOfHealth = 3;
	[Export] public BuildingType type;
	[Export] public int electricityCostPerTurn;
	[Export] public Array<ResourceType> requiredResources;
	[Export] public int[] buildCosts;
	[Export] public int[] repairCosts;
}

public enum ResourceType {
	WOOD,
	ELECTRICITY,
	STEEL
}

public enum BuildingType {
	WALL,
	LAZER,
	LANDMINE,

	LUMBER_MILL,
	POWER_PLANT
}

public enum BuildingState {
	ACTIVE,
	BUILDING,
	DESTROYED,
}
