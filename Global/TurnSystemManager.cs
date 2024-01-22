using Godot;
using System;

public partial class TurnSystemManager : Node {
	// Blob Turn Counter
	int turn_counter = 0;
	
	[Export] public PackedScene WinScene;
	[Export] public PackedScene LossScene;
	
	public override void _Ready() {
		PlayerTurnManager.Instance.PlayerTurnEnded += StartBlobTurn;
		BlobTurnManager.Instance.BlobTurnEnded += StartPlayerTurn;
	}

	private void StartPlayerTurn() {
		turn_counter++;
		
		// Collect Resources
		PlayerResources.Instance.CollectResources();
		
		//foreach (IBuilding building in PlayerTurnManager.Instance.playerBuildings)
		//{
			//if (building.status == BuildingState.BUILDING)
			//{
				//// Update Constructions
				//building.Construct();
				//continue;
			//}
			//
			//// Execute Defense Actions
			//switch (building.buildingData.type)
			//{
				//case (BuildingType.FREEZERAY):
				//case (BuildingType.LASER):
				//{
					//((IActiveDefense) building).Action();
					//break;
				//}
			//}
		//} 
	}

	private void StartBlobTurn() {
		//CheckVictoryConditions();
		
		BlobTurnManager.Instance.BlobTurn();
	}
	
	public void CheckVictoryConditions(bool player_concede = false)
	{
		int n_blobs = BlobTurnManager.Instance.Blobs.Count;
		int n_tiles = (3 * MapManager.Instance.bound * (MapManager.Instance.bound + 1)) + 1;
		
		GD.Print($"Turn({turn_counter}) N_Blobs({n_blobs}) TileTarget({n_tiles * 0.75})");
		
		// WIN Conditions
		if (
			// Blob Destroyed
			(n_blobs == 0) || 
			// Blob Turn Timeout - Prevent Stalled Games
			(n_blobs < turn_counter / 2) 
			)
		{
			GD.Print("PLAYER WIN");
			GetTree().ChangeSceneToPacked(WinScene);
		}
		
		
		// LOSS Conditions
		if (
			// Player Concede
			player_concede ||
			// Blob Majority
			(n_blobs > 0.75 * n_tiles)
			)
		{
			GD.Print("BLOB WIN");
			GetTree().ChangeSceneToPacked(LossScene);
		}
	}
}
