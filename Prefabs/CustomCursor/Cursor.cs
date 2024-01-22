using Godot;
using System;

public partial class Cursor : Node2D {
	public static Cursor Instance;
	public override void _Ready() {
		if(Instance == null) {
			Instance = this;
		}
	}

    public override void _Process(double delta) {
		Vector2I gridPosition = GetMouseGridPosition();

		if(MapManager.Instance.InBounds(gridPosition)) {
			Show();
			Position = MapManager.Instance.tilemap.MapToLocal(gridPosition);
		} else {
			Hide();
		}
		
    }

	public Vector2I GetMouseGridPosition() {
		return MapManager.Instance.tilemap.LocalToMap(GetGlobalMousePosition());
	}
}
