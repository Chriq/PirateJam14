using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerTurnManager : Node {
	public static PlayerTurnManager Instance;
	
	// Building nodes 
	[Export] public PackedScene[] BuildingScenes;
	[Export] private PackedScene selectedItemToBuild;

	// List of all active building
	public List<Node2D> Buildings;

	[Signal]
	public delegate void PlayerTurnEndedEventHandler();
	
	public override void _Ready() {
		if(Instance == null)
			Instance = this;
	}

	public override void _Process(double delta) {
		if(Input.IsMouseButtonPressed(MouseButton.Left) && selectedItemToBuild != null) {
			Build(selectedItemToBuild);
		}

		if(Input.IsMouseButtonPressed(MouseButton.Right)) {
			selectedItemToBuild = null;
		}
	}

	public void SelectItemToBuild(PackedScene building) {
		selectedItemToBuild = building;
	}

	public void Build(PackedScene building) {
		Vector2 mousePosition = GetViewport().GetMousePosition();
		MapManager.Instance.BuildOnTile(mousePosition, building);
		selectedItemToBuild = null;
	}

	public void EndTurn() {
		EmitSignal(SignalName.PlayerTurnEnded);
	}
}
