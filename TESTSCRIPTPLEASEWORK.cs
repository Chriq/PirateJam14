using Godot;
using System;

public enum BlobActions
{
	Grow,
	Launch,
	Eat	
}
public enum Occupancy
{
	Unoccupied,
	Building,
	Blob
}
unsafe public struct GridSquare
{
	public Occupancy Occupied;
	public Node2D* Occupier;
}

public partial class TESTSCRIPTPLEASEWORK : Node2D
{
	[Export] public PackedScene[] BuildingScenes;
	[Export] public PackedScene BlobScene;
	
	[ExportGroup("Grid Layout")]
	[Export] public int square_size = 60;
	[Export] public double bound = 6;
	[Export] public int grid_x = 12;
	[Export] public int grid_y = 12;
	 
	private GridSquare[] grid;
	private int blobcount;
//	private Blob[] blobs;
	
	private double timepass = 0;
	
	public override void _Ready()
	{
		GD.Randomize();
		
		GridInit();
		BlobInit();
	}

	public override void _Process(double delta)
	{
		timepass += delta;
		if (timepass > 5)
		{
			timepass -= 5;
			BlobTurn();
		}
	}
	
	private bool CheckRoad(int i)
	{
		if (grid_x % 2 == 1)
			return
				i % grid_x == grid_x / 2      || 
				i / grid_x == grid_y / 2;
		else
			return
				i % grid_x == grid_x / 2      ||
				i % grid_x == (grid_x / 2) - 1  || 
				i / grid_x == grid_y / 2      || 
				i / grid_x == (grid_y / 2 - 1);
	}
	private bool CheckBound(int i)
	{
		double x = grid_x / 2 - 0.5 - i % grid_x;
		double y = grid_y / 2 - 0.5 - i / grid_x;
		
		return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) > bound;
	}
	unsafe public void GridInit()
	{
		int squares = grid_x * grid_y;
		
		grid = new GridSquare[squares];		
		
		for (int i = 0; i < squares; i++)
		{
			// Pass Conditions
			if(CheckRoad(i))
				continue;
			if(CheckBound(i))
				continue;
			
			// Select Random Building
			PackedScene obj = BuildingScenes[GD.Randi() % BuildingScenes.Length];
			
			// Instantiate
			Node2D newobj = (Node2D) obj.Instantiate();
			newobj.Position = new Vector2(square_size * (i % grid_x), square_size * (i / grid_x));
			AddChild(newobj);
			
			//Update Grid
			grid[i].Occupied = Occupancy.Building;
			grid[i].Occupier = &newobj;
		}
	}
	unsafe public void BlobInit()
	{
		if (grid_x % 2 == 0)
			for (int x = grid_x / 2 - 1; x < grid_x / 2 + 1; x++)
			{
				for (int y = grid_y / 2 - 1; y < grid_y / 2 + 1; y++)
				{
					// Instantiate
					Node2D blob = (Node2D) BlobScene.Instantiate();
					blob.Position = new Vector2(square_size * x, square_size * y);
					AddChild(blob);
					
					//Update Grid
					grid[y * grid_x + x].Occupied = Occupancy.Blob;
					grid[y * grid_x + x].Occupier = &blob;
				}
			}
		else
			GD.Print("ODD BLOB NOT IMPLEMENTED");
	}
	unsafe private void BlobTurn()
	{
		GD.Print("BLOB TURN");
		switch (GD.Randi() % Enum.GetNames(typeof(BlobActions)).Length)
		{
			case (long) BlobActions.Grow:
				{
					GD.Print("Grow!");
					break;
				}
			case (long) BlobActions.Launch:
				{
					GD.Print("Launch!");
					for (int i = 0; i < 5; i++)
					{
						// Randomly Assign Position
						int pos = 0;
						while (CheckBound(pos) || grid[pos].Occupied == Occupancy.Blob)
						{
							pos = (int) GD.Randi() % (grid_x * grid_y);
						}
							
						//Instantiate 
						Node2D blob = (Node2D) BlobScene.Instantiate();
						blob.Position = new Vector2(square_size * (pos % grid_x), square_size * (pos / grid_x));
						AddChild(blob);
						
						//Update Grid
						grid[i].Occupied = Occupancy.Blob;
						grid[i].Occupier = &blob;
					}
					break;
				}
			case (long) BlobActions.Eat:
				{
					GD.Print("Eat?");
					break;
				}
		}
	}
}
