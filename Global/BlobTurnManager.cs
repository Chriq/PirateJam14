using Godot;
using System;
using System.Collections.Generic;

public enum BlobAction
{
	None,
	Thaw,
	Eat,
	Grow,
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
	
	public void BlobInit()
	{
		foreach (Vector2I gridPosition in blob_initial_indices)
		{
			SpawnBlob(gridPosition);
		}
	}
	
	public void SpawnBlob(Vector2I gridPosition)
	{
		// Instantiate
		Blob blob = (Blob) (Node2D) BlobScene.Instantiate();
		blob.InitBlob(gridPosition);
		
		blob.Position = MapManager.Instance.tilemap.MapToLocal(gridPosition);
		
		// Update References
		Blobs.Add(blob);
		MapManager.Instance.AddBlobToMap(gridPosition, blob);
		
		// Update Surrounding
		foreach (Vector2I off in MapManager.Instance.surround_offsets[
			blob.position[1] % MapManager.Instance.surround_offsets.Length
			])
		{
			Blob neighbor = (Blob) MapManager.Instance.GetTile(off + gridPosition).occupierBlob;
			
			if (neighbor != null)
				neighbor.CheckSurrounded();	
		}
		
		// Insert
		MapManager.Instance.SpawnNode.AddChild(blob);
	}
		
	public void BlobTurn()
	{
		int n_blobs = Blobs.Count;
		for (int blob_i = 0; blob_i < n_blobs; blob_i++)
		{
			Blob blob = Blobs[blob_i];
			
			switch(blob.BlobTurn())
			{
				case(BlobAction.None):
					{
						break;
					}
				case(BlobAction.Thaw):
					{
						// TODO: Update Visuals to reflect thawed blob tile
						GD.Print("Thaw!");
						break;
					}
				case(BlobAction.Eat):
					{
						// TODO: Update building data - disable building
						GD.Print("Eat!");
						break;
					}
				case(BlobAction.Grow):
					{
						int off_ind = blob.position[1] % MapManager.Instance.surround_offsets.Length;
						int len = MapManager.Instance.surround_offsets[off_ind].Length;
						int rand = (int) (GD.Randi() % len);
						
						for (int i = 0; i < len; i++)
						{
							Vector2I pos = MapManager.Instance.surround_offsets[off_ind][(i + rand) % len] + blob.position;							
							
							if (MapManager.Instance.GetTile(pos).occupierBlob == null)
							{
								SpawnBlob(pos);
								break;
							}
							
						}
						break;
					}
				case(BlobAction.Launch):
					{
						Vector2I pos = new Vector2I(0, 0);
						
						// TODO: Update Bounds checking
						for (int i = 0; i < 5; i++)
						{
							pos = new Vector2I(
								(int) (GD.Randi() % MapManager.Instance.grid_x), 
								(int) (GD.Randi() % MapManager.Instance.grid_y)
								);
							
							if (MapManager.Instance.GetTile(pos).occupierBlob == null)
								break;
						}
						
						if (MapManager.Instance.GetTile(pos).occupierBlob == null)
							SpawnBlob(pos);
						break;
					}
			}
		}
		
		EmitSignal(SignalName.BlobTurnEnded);
	}
}
