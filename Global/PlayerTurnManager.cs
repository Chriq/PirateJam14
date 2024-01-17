using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerTurnManager : Node {
	public static PlayerTurnManager Instance;
	
	// Building nodes 
	[Export] public PackedScene[] BuildingScenes;
	[Export] private Building test;

	// List of all active building
	public List<Node2D> Buildings;
	
	public override void _Ready() {
		if(Instance == null) {
			GD.Print("SINGLE!!!");
			Instance = this;
		}
		else{
			GD.Print("MULTPLE SINGLETON!!!");
		}
	}

	public override void _Process(double delta) {
		//if(Input.IsMouseButtonPressed(MouseButton.Left)) {
			//Build(test);
		//}
	}

	public void SelectItemToBuild(Building building) {

	}

	//public void Build(Building building) {
		//Vector2 mousePosition = GetViewport().GetMousePosition();
		//MapManager.Instance.BuildOnTile(mousePosition, building);
	//}
}
