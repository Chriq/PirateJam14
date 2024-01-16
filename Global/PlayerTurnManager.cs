using Godot;
using System;

public partial class PlayerTurnManager : Node {
	public static PlayerTurnManager Instance;
	[Export] private Building test;

	public override void _Ready() {
		if(Instance == null) {
			Instance = this;
		}
	}

	public override void _Process(double delta) {
		if(Input.IsMouseButtonPressed(MouseButton.Left)) {
			Build(test);
		}
	}

	public void SelectItemToBuild(Building building) {

	}

	public void Build(Building building) {
		Vector2 mousePosition = GetViewport().GetMousePosition();
		MapManager.Instance.BuildOnTile(mousePosition, building);
	}
}