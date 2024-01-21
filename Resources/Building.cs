using Godot;
using Godot.Collections;

public partial class Building : Resource
{
	[Export] public int maxTurnsOfHealth = 3;
	[Export] public BuildingType type;
	[Export] public int electricityCostPerTurn;
	[Export] private Array<ResourceType> requiredResources;
	[Export] private int[] buildCosts;
	[Export] private int[] repairCosts;

	public Dictionary<ResourceType, int> buildCost = new();
	public Dictionary<ResourceType, int> repairCost = new();

	public Building(int max, BuildingType typ, int elec, Array<ResourceType> req, int[] build, int[] rep) {
		for(int i = 0; i < requiredResources.Count; i++) {
			buildCost.Add(requiredResources[i], buildCosts[i]);
			repairCost.Add(requiredResources[i], repairCosts[i]);
		}
	}
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
