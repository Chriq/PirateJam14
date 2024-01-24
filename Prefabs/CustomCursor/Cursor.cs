using Godot;
using System;

public partial class Cursor : Node2D {
	public static Cursor Instance;

	private Sprite2D selectedTile = null;
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

	public void HighlightSeectedTile() {
		DeselectTile();
		selectedTile = new();
		selectedTile.Centered = true;
		selectedTile.Texture = GetChild<Sprite2D>(0).Texture;
		selectedTile.Position = MapManager.Instance.tilemap.MapToLocal(GetMouseGridPosition());
		GetTree().Root.AddChild(selectedTile);
	}

	public void DeselectTile() {
		if(selectedTile != null) {
			selectedTile.Hide();
		}
	}
}
