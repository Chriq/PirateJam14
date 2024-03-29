using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerTurnManager : Node {
	public static PlayerTurnManager Instance;
	
	[Export] public Texture2D ConstructionSprite;
	
	// Building nodes
	private PackedScene selectedItemToBuild;
	public HexNode selectedHexNode;
	public Vector2I selectedTileIndex;

	// List of all active building
	public List<Node2D> playerBuildings = new();
	[Export] private Node initialBuildingContainer;

	[Signal]
	public delegate void PlayerTurnEndedEventHandler();
	[Signal]
	public delegate void HexNodeSelectedEventHandler();
	
	public override void _Ready() {
		if(Instance == null)
			Instance = this;

		foreach(IBuilding building in initialBuildingContainer.GetChildren()) {
			playerBuildings.Add(building);
			
			Vector2I gridPosition = MapManager.Instance.tilemap.LocalToMap(building.Position);
			MapManager.Instance.AddBuildingToMap(gridPosition, building);
			building.currentBuild = 0;
		}
	}
	public override void _ExitTree()
	{
		Instance = null;
	}

	public override void _Process(double delta) {
		Vector2I mousegridPosition = Cursor.Instance.GetMouseGridPosition();
		if(Input.IsMouseButtonPressed(MouseButton.Left) && MapManager.Instance.InBounds(mousegridPosition)) {
			//Build(selectedItemToBuild);
			selectedHexNode = default;
			selectedTileIndex = mousegridPosition;
			MapManager.Instance.map.TryGetValue(mousegridPosition, out selectedHexNode);
			Cursor.Instance.HighlightSeectedTile();
			EmitSignal(SignalName.HexNodeSelected);
		}
	}

	public void SelectItemToBuild(PackedScene building) {
		selectedItemToBuild = building; 
	}
	
	public void EndTurn() {
		EmitSignal(SignalName.PlayerTurnEnded);
	}
}
