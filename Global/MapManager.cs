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
	[Export] public int bound = 6;
	[Export] public Vector2I center = new Vector2I(6,7);

	[Signal] public delegate void BuildingAddedEventHandler();
	
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
	
	// Grid Navigation Tools
	public bool InBounds(Vector2I position)
	{
		float x = position.X - center.X;
		float y = Math.Abs(position.Y - center.Y);
		
		if (center.Y % 2 == 1)
			return
				-bound + y/2 <= x && x <= bound - Math.Floor(y/2) &&
				y <= bound; 
		
		return
			-bound + Math.Floor(y/2) <= x && x <= bound - y/2 &&
			y <= bound;
	}
	public Vector2I[] GetOffsets(Vector2I position)
	{
		return surround_offsets[position.Y % 2];
	}
	
	// Node-level Functions
	public HexNode GetTile(Vector2I position)
	{
		if (map.ContainsKey(position))
			return map[position];
		
		HexNode node = new HexNode(null, null);
		
		map.Add(position, node);
		
		return node;
	}
	public void SetTile(Vector2I position, IBuilding building = null, Blob blob = null)
	{
		if (map.ContainsKey(position))
			map[position] = new HexNode(building, blob);
		
		else
			map.Add(position, new HexNode(building, blob));
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
	
	public void BuildOnTile(HexNode tile, PackedScene building) {
		Vector2I cell = PlayerTurnManager.Instance.selectedTileIndex;
		IBuilding build = (IBuilding) (Node2D) building.Instantiate();
		bool sufficientResources = PlayerResources.Instance.SpendResourcesOnBuilding(build);
		
		if(!sufficientResources)
			GD.Print("INSUFFICIENT RESOURCES");
		
		build.Position = tilemap.MapToLocal(cell);
		SpawnNode.AddChild(build);
		AddBuildingToMap(cell, build);
		Cursor.Instance.DeselectTile();
		EmitSignal(SignalName.BuildingAdded);
		
		PlayerTurnManager.Instance.playerBuildings.Add(build);
	}

	public void RepairBuildingOnTile(HexNode tile) {
		PlayerResources.Instance.SpendResourcesRepairingBuilding(tile.occupierBuilding);
		tile.occupierBuilding.currentHealth++;
		tile.occupierBuilding.UpdateDamageSprite();
	}

	public void AddBlobToMap(Vector2I gridPosition, Blob blob) {
		IBuilding building = GetTile(gridPosition).occupierBuilding;
		
		SetTile(gridPosition, building, blob);
		
		//HexNode tile;
		//if(map.TryGetValue(gridPosition, out tile)) {
			//tile.occupierBlob = blob;
			//map[gridPosition] = tile;
		//} else {
			//map.Add(gridPosition, new HexNode(null, blob));
		//}
	}
	public void RemoveBlobFromMap(Vector2I gridPosition)
	{
		IBuilding building = GetTile(gridPosition).occupierBuilding;
		
		SetTile(gridPosition, building);
		
		//HexNode tile = GetTile(gridPosition);
		//
		//SpawnNode.RemoveChild(tile.occupierBlob);
		//
		//tile.occupierBlob = null;
	}
	
	public void AddBuildingToMap(Vector2I gridPosition, IBuilding building) {
		SetTile(gridPosition, building);
		//HexNode tile;
		//if(map.TryGetValue(gridPosition, out tile)) {
			//tile.occupierBuilding = building;
			//map[gridPosition] = tile;
		//} else {
			//map.Add(gridPosition, new HexNode(building, null));
		//}
	}
}
