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
	double time = 0;
	
	public override void _Ready()
	{
		if(Instance == null) {
			Instance = this;
		}
		Blobs = new List<Blob>();
		BlobInit();
	}
	public override void _Process(double delta)
	{
		time += delta;
		if (time > 5)
		{
			time -= 1;
			BlobTurn();
		}
	}

	// Blob Prefab
	[Export] public PackedScene BlobScene;
	
	(int, int)[] blob_initial_indices = {(0,0)};
	
	// Blob List
	public List<Blob> Blobs;
	
	public void BlobInit()
	{
		foreach ((int x, int y) in blob_initial_indices)
		{
			// Instantiate
			Blob blob = (Blob) (Node2D) BlobScene.Instantiate();
			blob.InitBlob(false, false);
			
			blob.Position = new Vector2(MapManager.Instance.square_size * x, MapManager.Instance.square_size * y);
			
			// Update References
			Blobs.Add(blob);
			//MapManager.Instance.map.Add(new Vector2I(x, y), blob);
			
			// Insert
			MapManager.Instance.SpawnNode.AddChild(blob);			
		}
	}
		
	public void BlobTurn()
	{
		foreach (Blob blob in Blobs)
		{
			switch(blob.BlobTurn())
			{
				case(BlobAction.None):
					{
						GD.Print("None!");
						break;
					}
				case(BlobAction.Thaw):
					{
						GD.Print("Thaw!");
						break;
					}
				case(BlobAction.Eat):
					{
						GD.Print("Eat!");
						break;
					}
				case(BlobAction.Grow):
					{
						GD.Print("Grow!");
						break;
					}
				case(BlobAction.Launch):
					{
						GD.Print("Launch!");
						break;
					}
			}
		}
	}
}
