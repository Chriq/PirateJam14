using Godot;
using System;

public partial class MapManager : Node {
	public static MapManager Instance;

	[Export] public TileMap tilemap;

	public override void _Ready() {
		if(Instance == null) {
			Instance = this;
		}
	}

	public void BuildOnTile(Vector2 mousePosition, Building building) {
		Vector2I cell = tilemap.LocalToMap(mousePosition);
		tilemap.SetCell(0, cell, 0, building.atlasIndex);

	}
}
