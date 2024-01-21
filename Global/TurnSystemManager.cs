using Godot;
using System;

public partial class TurnSystemManager : Node {
	public override void _Ready() {
		PlayerTurnManager.Instance.PlayerTurnEnded += StartBlobTurn;
		BlobTurnManager.Instance.BlobTurnEnded += StartPlayerTurn;
	}

	private void StartPlayerTurn() {
		// Collect Resources
		PlayerResources.Instance.CollectResources();
		
		foreach (IBuilding building in PlayerTurnManager.Instance.playerBuildings)
		{
			if (building.status == BuildingState.BUILDING)
			{
				// Update Constructions
				building.Construct();
				continue;
			}
			
			// Execute Defense Actions
			switch (building.buildingData.type)
			{
				case (BuildingType.LASER):
				{
					((DefenseLaser) building).Action();
					break;
				}
			}
		} 
	}

	private void StartBlobTurn() {
		BlobTurnManager.Instance.BlobTurn();
	}
}
