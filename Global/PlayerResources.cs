using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerResources : Node {
	public static PlayerResources Instance;

	public Dictionary<ResourceType, int> playerResourceCounts = new();

    public override void _Ready() {
		if(Instance == null) {
			Instance = this;
		}

		foreach(ResourceType type in Enum.GetValues(typeof(ResourceType))) {
			playerResourceCounts.Add(type, 0);
		}
        /*playerResourceCounts = new Dictionary<ResourceType, int> {
			{ ResourceType.WOOD, 0 },
			{ ResourceType.ELECTRICITY, 0 }
		};*/
    }

    public void CollectResources() {
		foreach(Node2D buildingNode in PlayerTurnManager.Instance.playerBuildings) {
			GD.Print("Collecting ");
			
			IBuilding building = (IBuilding) buildingNode;
			if(building.currentHealth > 0) {
				Dictionary<ResourceType, int> yields = building.YieldResources();
				foreach(ResourceType key in yields.Keys) {
					playerResourceCounts[key] += yields[key];
					GD.Print($"{yields[key]} {Enum.GetName(typeof(ResourceType), key)} from {building.Name}");
				}
			}
		}
	}
}
