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
		
		// Update Constructions
		foreach (IBuilding building in PlayerTurnManager.Instance.playerBuildings)
		{
			if (building.status == BuildingState.BUILDING)
				building.Construct();
		} 
		
		// Execute Defense Actions
		
	}

	private void StartBlobTurn() {
		BlobTurnManager.Instance.BlobTurn();
	}
}
