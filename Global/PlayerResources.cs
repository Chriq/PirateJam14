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
			playerResourceCounts.Add(type, 0);
		}

		CollectResources();
	}
	public override void _ExitTree()
	{
		Instance = null;
	}
	
	public void CollectResources() {
		foreach(IBuilding buildingNode in PlayerTurnManager.Instance.playerBuildings) {
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
				}
			}
		}

		CalculateElectricity();

		EmitSignal(SignalName.ResourcesChanged);
	}

	public bool CanAffordBuilding(Building building) {
		Dictionary<ResourceType, int> buildCosts = building.GetBuildCost();
		foreach(ResourceType resource in buildCosts.Keys) {
			if(playerResourceCounts[resource] < buildCosts[resource]) {
				return false;
			}
		}

		return true;
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

	public bool SpendResourcesRepairingBuilding(Node2D buildingObject) {
		IBuilding building = (IBuilding) buildingObject;
		Dictionary<ResourceType, int> repairCosts = building.GetRepairCost();
		foreach(ResourceType resource in repairCosts.Keys) {
			if(playerResourceCounts[resource] >= repairCosts[resource]) {
				playerResourceCounts[resource] = playerResourceCounts[resource] - repairCosts[resource];
			} else {
				return false;
			}
		}

		EmitSignal(SignalName.ResourcesChanged);
		return true;
	}

	public void CalculateElectricity() {
		int totalUsedElectricity = GetTotalUsedElectricity();
		int totalElectricityYield = GetTotalElectricityYield();
		if(totalUsedElectricity > totalElectricityYield) {
			List<IBuilding> poweredBuildings = GetActivePoweredBuildings();
			int numBuildingsToDisable = GetNumberOfBuildingsToDisable(poweredBuildings, totalUsedElectricity, totalElectricityYield);
		
			for(int i = 0; i < numBuildingsToDisable; i++) {
				int index = GD.RandRange(0, poweredBuildings.Count-1);
				if(index < 0) index = 0;

				poweredBuildings[index].status = BuildingState.UNPOWERED;
				poweredBuildings[index].UpdateDamageSprite();
				poweredBuildings.RemoveAt(index);
			}
		}

		playerResourceCounts[ResourceType.ELECTRICITY] = totalElectricityYield - totalUsedElectricity;
	}

	private int GetNumberOfBuildingsToDisable(List<IBuilding> poweredBuildings, int totalUsedElectricity, int totalElectricityYield) {
		int energyBalance = totalUsedElectricity;
		int numBuildingsToTurnOff = 0;
		foreach(IBuilding building in poweredBuildings) {
			energyBalance -= building.buildingData.electricityCostPerTurn;
			numBuildingsToTurnOff++;
			
			if(energyBalance <= totalElectricityYield) {
				break;
			}
		}

		return numBuildingsToTurnOff;
	}

	private List<IBuilding> GetActivePoweredBuildings() {
		List<IBuilding> poweredBuildings = new();
		foreach(IBuilding building in PlayerTurnManager.Instance.playerBuildings) {
			if(building.status == BuildingState.ACTIVE && building.buildingData.electricityCostPerTurn > 0) {
				poweredBuildings.Add(building);
			}
		}

		return poweredBuildings;
	}

	private int GetTotalElectricityYield() {
		int totalEnergyYield = 0;
		foreach(IBuilding building in PlayerTurnManager.Instance.playerBuildings) {
			if(building.status == BuildingState.ACTIVE && building.buildingData.type == BuildingType.POWER_PLANT) {
				totalEnergyYield += ((IResourceBuilding) building).yieldAmt;
			}
		}

		return totalEnergyYield;
	}

	private int GetTotalUsedElectricity() {
		int totalEnergyCost = 0;
		foreach(IBuilding building in PlayerTurnManager.Instance.playerBuildings) {
			// Reset Unpowered buildings
			if(building.status == BuildingState.UNPOWERED) {
				building.status = BuildingState.ACTIVE;
				building.UpdateDamageSprite();
			}

			if(building.status == BuildingState.ACTIVE) {
				totalEnergyCost += building.buildingData.electricityCostPerTurn;
			}
		}

		return totalEnergyCost;
	}
}
