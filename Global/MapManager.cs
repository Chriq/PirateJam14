using Godot;
using System;
using System.Collections.Generic;

public struct HexNode {
	public Node2D occupierBuilding;
	public Node2D occupierBlob;

	public HexNode() {
		occupierBuilding = null;
		occupierBlob = null;
	}

	public HexNode(Node2D building, Node2D blob) {
		occupierBuilding = building;
		occupierBlob = blob;
	}
}

public partial class MapManager : Node {
	public static MapManager Instance;

	[Export] public Node2D SpawnNode;
	
	[Export] public TileMap tilemap;
	
	[ExportGroup("Grid Layout")]
	[Export] public int square_size = 60;
	[Export] public double bound = 6;
	[Export] public int grid_x = 12;
	[Export] public int grid_y = 12;
	
	public Dictionary<Vector2I, HexNode> map = new();

	public override void _Ready() {
		if(Instance == null) {
			Instance = this;
		}
	}

	public void BuildOnTile(Vector2 mousePosition, PackedScene building) {
		Vector2I cell = tilemap.LocalToMap(mousePosition);
		Node2D build = (Node2D) building.Instantiate();
		build.Position = tilemap.MapToLocal(cell);
		SpawnNode.AddChild(build);
		AddBuildingToMap(cell, build);
	}

	public void AddBlobToMap(Vector2I gridPosition, Node2D blob) {
		HexNode tile;
		if(map.TryGetValue(gridPosition, out tile)) {
			tile.occupierBlob = blob;
			map[gridPosition] = tile;
		} else {
			map.Add(gridPosition, new HexNode(null, blob));
		}
	}

	public void AddBuildingToMap(Vector2I gridPosition, Node2D building) {
		HexNode tile;
		if(map.TryGetValue(gridPosition, out tile)) {
			tile.occupierBuilding = building;
			map[gridPosition] = tile;
		} else {
			map.Add(gridPosition, new HexNode(building, null));
		}
	}
}
