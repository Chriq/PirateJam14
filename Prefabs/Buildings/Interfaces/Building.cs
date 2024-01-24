using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class Building : Resource
{
	[Export] public int maxTurnsOfHealth = 3;
	[Export] public BuildingType type;
	[Export] public int electricityCostPerTurn;
	[Export] public Array<ResourceType> requiredResources;
	[Export] public int[] buildCosts;
	[Export] public int[] repairCosts;
	[Export] public string description;
	[Export] public int buildTimer = 0;
	[Export] public Texture2D damageSprite;
	[Export] public Texture2D destroyedSprite;
	[Export] public Texture2D needsElectricitySprite;

	public System.Collections.Generic.Dictionary<ResourceType, int> buildCost;
	public System.Collections.Generic.Dictionary<ResourceType, int> repairCost;

	public System.Collections.Generic.Dictionary<ResourceType, int> GetBuildCost() {
		if(buildCost == null) {
			buildCost = new();
			for(int i = 0; i < requiredResources.Count; i++) {
				if(i < buildCosts.Length) buildCost.Add(requiredResources[i], buildCosts[i]);
			}
		}

		return buildCost;
	}

	public System.Collections.Generic.Dictionary<ResourceType, int> GetRepairCost() {
		if(repairCost == null) {
			repairCost = new();
			for(int i = 0; i < requiredResources.Count; i++) {
				if(i < repairCosts.Length) repairCost.Add(requiredResources[i], repairCosts[i]);
			}
		}

		return repairCost;
	}
}

public enum ResourceType {
	WOOD,
	ELECTRICITY,
	STEEL,
	OIL
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
	POWER_PLANT,
	STEEL_MILL,
	OIL_REFINERY
}

public enum BuildingState {
	ACTIVE,
	BUILDING,
	DESTROYED,
	UNPOWERED
}
