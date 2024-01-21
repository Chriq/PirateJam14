using Godot;
using System;

public partial class Blob : Node2D
{
	// Grid Location Indices
	public Vector2I position { get; private set; }
	
	// Frozen Sprite
	Sprite2D FrozenSprite = null;
	
	// Blob state
	public int freeze_counter { get; private set; } = 0;
	private int counter = 0;
	
	public override void _Ready()
	{
		FrozenSprite = new Sprite2D();
		FrozenSprite.Texture = BlobTurnManager.Instance.FrozenSprite;
	}
	
	public void InitBlob(Vector2I GridPosition, int init_counter)
	{
		position = GridPosition;
		counter = init_counter;
	}
	
	public void Freeze(int freeze)
	{
		if (freeze_counter > 0)
		{
			freeze_counter += freeze;
		}
		else
		{
			freeze_counter = freeze;
			AddChild(FrozenSprite);
		}		
	}
	public void SetCounter(int count)
	{
		counter = count;
	}
	
	public bool BlobTurn()
	{
		// Frozen - Do not update counter
		if (freeze_counter > 0)
		{
			if (--freeze_counter <= 0)
				RemoveChild(FrozenSprite);

			return false;
		}	
		
		// Decrement counter
		return counter-- <= 0;
	}
}
