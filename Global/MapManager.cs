using Godot;
using System;
using System.Collections.Generic;

public struct HexNode {
	public IBuilding occupierBuilding;
	public Blob occupierBlob;

	public HexNode() {
		occupierBuilding = null;
		occupierBlob = null;
	}

	public HexNode(IBuilding building, Blob blob) {
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
	
	public Vector2I[][] surround_offsets =
	{
		new Vector2I[]
		{
			new Vector2I(-1, -1),
			new Vector2I(-1, 0),
			new Vector2I(-1, 1),
			new Vector2I(0, 1),
			new Vector2I(1, 0),
			new Vector2I(0, -1)
		},
		new Vector2I[]
		{
			new Vector2I(0, -1),
			new Vector2I(-1, 0),
			new Vector2I(0, 1),
			new Vector2I(1, 1),
			new Vector2I(1, 0),
			new Vector2I(1, -1)
		},
	};
	
	public override void _Ready() {
		if(Instance == null)
			Instance = this;
	}
	
	public HexNode GetTile(Vector2I position)
	{
		if (map.ContainsKey(position))
			return map[position];
		
		HexNode node = new HexNode(null, null);
		
		map.Add(position, node);
		
		return node;
	}
	public Vector2I[] GetOffsets(Vector2I position)
	{
		return surround_offsets[position[1] % 2];
	}
	
	public void DeleteBuilding(Vector2I position)
	{
		IBuilding building = map[position].occupierBuilding;
		
		// Map
		map.Remove(position);
		
		// Blobs List
		PlayerTurnManager.Instance.playerBuildings.Remove(building);
		
		// Instance
		building.QueueFree();
	}
	
	public Node2D BuildOnTile(Vector2 mousePosition, PackedScene building) {
		Vector2I cell = tilemap.LocalToMap(mousePosition);
		IBuilding build = (IBuilding) (Node2D) building.Instantiate();
		if(PlayerResources.Instance.SpendResourcesOnBuilding(build)) {
			build.Position = tilemap.MapToLocal(cell);
			SpawnNode.AddChild(build);
			AddBuildingToMap(cell, build);

			return build;
		}
		
		return null;
	}

	public void AddBlobToMap(Vector2I gridPosition, Blob blob) {
		HexNode tile;
		if(map.TryGetValue(gridPosition, out tile)) {
			tile.occupierBlob = blob;
			map[gridPosition] = tile;
		} else {
			map.Add(gridPosition, new HexNode(null, blob));
		}
	}
	public void RemoveBlobFromMap(Vector2I gridPosition)
	{
		HexNode tile = map[gridPosition];
		
		SpawnNode.RemoveChild(tile.occupierBlob);
		
		tile.occupierBlob = null;
	}
	
	public void AddBuildingToMap(Vector2I gridPosition, IBuilding building) {
		HexNode tile;
		if(map.TryGetValue(gridPosition, out tile)) {
			tile.occupierBuilding = building;
			map[gridPosition] = tile;
		} else {
			map.Add(gridPosition, new HexNode(building, null));
		}
	}
}
