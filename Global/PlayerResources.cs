using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerResources : Node {
	public static PlayerResources Instance;

	public Dictionary<ResourceType, int> playerResourceCounts = new();

	[Signal] 
	public delegate void ResourcesChangedEventHandler();

	public override void _Ready() {
		if(Instance == null) {
			Instance = this;
		}

		foreach(ResourceType type in Enum.GetValues(typeof(ResourceType))) {
			playerResourceCounts.Add(type, 30);
		}
	}

	public void CollectResources() {
		foreach(IBuilding buildingNode in PlayerTurnManager.Instance.playerBuildings) {
			GD.Print("Collecting ");

			if (
				buildingNode is IResourceBuilding resourceBuilding
				&& 
				buildingNode.status == BuildingState.ACTIVE
				) 
			{
				IResourceBuilding building = (IResourceBuilding) buildingNode;
				
				Dictionary<ResourceType, int> yields = building.YieldResources();
				foreach(ResourceType key in yields.Keys) {
					playerResourceCounts[key] += yields[key];
					GD.Print($"{yields[key]} {Enum.GetName(typeof(ResourceType), key)} from {building.Name}");
				}
			}
		}

		EmitSignal(SignalName.ResourcesChanged);
	}

	public bool SpendResourcesOnBuilding(Node2D buildingObject) {
		IBuilding building = (IBuilding) buildingObject;
		Dictionary<ResourceType, int> buildCosts = building.GetBuildCost();
		foreach(ResourceType resource in buildCosts.Keys) {
			if(playerResourceCounts[resource] >= buildCosts[resource]) {
				playerResourceCounts[resource] = playerResourceCounts[resource] - buildCosts[resource];
			} else {
				return false;
			}
		}

		EmitSignal(SignalName.ResourcesChanged);
		return true;
	}
}
