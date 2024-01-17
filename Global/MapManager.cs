using Godot;
using System;
using System.Collections.Generic;

unsafe public struct HexNode
{
	bool occupied_building;
	bool occupied_blob;
	
	Node2D* occupier_building;
	Node2D* occupier_blob;
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
	
	public Dictionary<Vector2I, HexNode> map;

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
	}
}
