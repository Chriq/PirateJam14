using Godot;
using System;

public partial class Blob : Node2D
{
	public Vector2I position;
	
	// Blob state
	private int freeze_counter = 0;
	private int counter = 0;
	
	public void InitBlob(Vector2I GridPosition, int init_counter)
	{
		position = GridPosition;
		counter = init_counter;
	}
	
	public void Freeze(int freeze)
	{
		freeze_counter = freeze;
	}
	public void SetCounter(int count)
	{
		counter = count;
	}
	
	public bool BlobTurn()
	{
		GD.Print($"Blob [{position}] - Freeze({freeze_counter}) Counter({counter})");
		
		// Frozen - Do not update counter
		if (freeze_counter > 0)
		{
			if (--freeze_counter <= 0)
				// TODO: Update Visual Freeze Indicator

			return false;
		}	
		
		// Decrement counter
		return --counter <= 0;
	}
}
