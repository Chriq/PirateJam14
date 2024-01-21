using Godot;
using System;
using System.Collections.Generic;

public enum BlobAction
{
	Eat_Building,
	Grow,
	Eat_Wall,
	Launch,
}

public partial class BlobTurnManager : Node
{
	// Singleton
	public static BlobTurnManager Instance;
	
	public override void _Ready()
	{
		if(Instance == null)
			Instance = this;
			
		Blobs = new List<Blob>();
		BlobInit();
	}
	
	// Blob Turn Signal
	[Signal] public delegate void BlobTurnEndedEventHandler();
	
	// Blob Prefab
	[Export] public PackedScene BlobScene;
	
	// Initial Blob Spawn
	[Export] public Godot.Collections.Array<Vector2I> blob_initial_indices = new Godot.Collections.Array<Vector2I>
	{
		new Vector2I(5,5),
		new Vector2I(5,6),
		new Vector2I(6,6),
	};
	
	// Blob List
	public List<Blob> Blobs;
	
	// Blob Sliders
	[ExportGroup("Blob Turn Behavior")]
	[Export] int blob_initial_counter = 6;
	[Export] int blob_freeze_counter = 4;
	[Export] int blob_eat_counter = 5;
	[Export] int blob_grow_counter = 5;
	[Export] int blob_launch_counter = 5;
	
	private void BlobInit()
	{
		foreach (Vector2I gridPosition in blob_initial_indices)
		{
			SpawnBlob(gridPosition);
		}
	}
	
	private void SpawnBlob(Vector2I gridPosition)
	{
		// Instantiate
		Blob blob = (Blob) (Node2D) BlobScene.Instantiate();
		blob.InitBlob(gridPosition, (int) (GD.Randi() % blob_initial_counter));
		
		blob.Position = MapManager.Instance.tilemap.MapToLocal(gridPosition);
		
		// Update References
		Blobs.Add(blob);
		MapManager.Instance.AddBlobToMap(gridPosition, blob);
		
		// Insert
		MapManager.Instance.SpawnNode.AddChild(blob);
	}
		
	public void BlobTurn()
	{
		int n_blobs = Blobs.Count;
		for (int blob_i = 0; blob_i < n_blobs; blob_i++)
		{
			Blob blob = Blobs[blob_i];
			
			if (blob.BlobTurn())
			{
				Vector2I[] offsets = MapManager.Instance.surround_offsets[blob.position[1] % 2];
				int len = offsets.Length;
				int rand = (int) (GD.Randi() % len);
				
				BlobAction action = BlobAction.Launch;
				Vector2I target = new Vector2I(0,0);
				
				// Check neighboring cells for blob action
				for (int i = 0; i < len; i++)
				{
					Vector2I pos = offsets[(i + rand) % len] + blob.position;
					
					HexNode node = MapManager.Instance.GetTile(pos);
					
					if (node.occupierBuilding != null)
					{
						IBuilding building = (IBuilding) node.occupierBuilding;
						
						if (building.buildingData.type != BuildingType.WALL)
						{
							action = BlobAction.Eat_Building;
							target = pos;
							break;
						}
						if (building.buildingData.type == BuildingType.WALL
							&&
							(int) action > (int) BlobAction.Eat_Wall)
						{
							target = pos;
							action = BlobAction.Eat_Wall;
						}
					}
					
					else if (node.occupierBlob == null
						&&
						(int) action > (int) BlobAction.Grow)
					{
						target = pos;
						action = BlobAction.Grow;
					}
				}
				
				GD.Print($"Blob [{blob.position}] Action [{action}]");
				// Execute blob action
				switch(action)
				{
					case (BlobAction.Eat_Building):
					case (BlobAction.Eat_Wall):
					{
						((IBuilding) MapManager.Instance.GetTile(target).occupierBuilding).DamageHealth();
						blob.SetCounter(blob_eat_counter);
						break;
					}
					
					case (BlobAction.Grow):
					{
						SpawnBlob(target);
						blob.SetCounter(blob_grow_counter);
						break;
					}
					
					case (BlobAction.Launch):
					{
						for (int i = 0; i < 10; i++)
						{
							target = new Vector2I(
								(int) (GD.Randi() % MapManager.Instance.grid_x), 
								(int) (GD.Randi() % MapManager.Instance.grid_y)
								);
							
							if (MapManager.Instance.GetTile(target).occupierBlob == null)
								SpawnBlob(target);
								break;
						}
						blob.SetCounter(blob_launch_counter);
						break;
					}
				}
			}
		}
		
		EmitSignal(SignalName.BlobTurnEnded);
	}
}
