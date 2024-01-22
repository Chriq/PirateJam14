using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerTurnManager : Node {
	public static PlayerTurnManager Instance;
	
	[Export] public Texture2D ConstructionSprite;
	
	// Building nodes 
	[Export] public PackedScene[] BuildingScenes;
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
			GD.Print(gridPosition);
			MapManager.Instance.AddBuildingToMap(gridPosition, building);
		}
	}

	public override void _Process(double delta) {
		Vector2I mousegridPosition = Cursor.Instance.GetMouseGridPosition();
		if(Input.IsMouseButtonPressed(MouseButton.Left) && MapManager.Instance.InBounds(mousegridPosition)) {
			//Build(selectedItemToBuild);
			selectedHexNode = default;
			selectedTileIndex = mousegridPosition;
			MapManager.Instance.map.TryGetValue(mousegridPosition, out selectedHexNode);
			EmitSignal(SignalName.HexNodeSelected);
		}
	}

	public void SelectItemToBuild(PackedScene building) {
		selectedItemToBuild = building; 
	}

	public void Build(PackedScene building) {
		Vector2 mousePosition = GetViewport().GetMousePosition();
		Node2D newBuilding = MapManager.Instance.BuildOnTile(selectedHexNode, building);
		if(newBuilding != null) {
			playerBuildings.Add(newBuilding);
		}
		
		selectedItemToBuild = null;
	}

	public void EndTurn() {
		EmitSignal(SignalName.PlayerTurnEnded);
	}
}
