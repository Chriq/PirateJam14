using Godot;
using System;

public partial class Blob : Node2D
{
	[ExportGroup("Counter Behavior")]
	[Export] bool reset_counter_on_freeze = false;
	[Export] bool preserve_counter_on_surround = false;
	
	[ExportGroup("Blob Turn Behavior")]
	[Export] int blob_freeze_counter = 2;
	[Export] int blob_eat_counter = 3;
	[Export] int blob_grow_counter = 3;
	[Export] int blob_launch_counter = 5;
	
	// Turn counter
	private int freeze_counter = 0;
	private int counter = 0;
	
	// Blob state
	public Vector2I position;
	private bool frozen = false;
	private bool surrounded = false;
	private BlobAction state = BlobAction.Grow;
	
	public void InitBlob(Vector2I GridPosition)
	{
		position = GridPosition;
		
		bool on_building = MapManager.Instance.GetTile(GridPosition).occupierBuilding != null;
		
		bool is_surrounded = CheckSurrounded();
		
		if(on_building)
		{
			state = BlobAction.Eat;
			counter = blob_eat_counter;
		}
			
		else if (is_surrounded)
		{
			state = BlobAction.Launch;
			counter = blob_launch_counter;
		}
			
		else
		{
			state = BlobAction.Grow;
			counter = blob_grow_counter;
		}
	}
	
	public void Freeze()
	{
		frozen = true;
		freeze_counter = blob_freeze_counter;
		
		if (reset_counter_on_freeze)
		{
			switch(state)
			{
				case BlobAction.Eat:
					{
						counter = blob_eat_counter;
						break;
					}
				case BlobAction.Grow:
					{
						counter = blob_grow_counter;
						break;
					}
				case BlobAction.Launch:
					{
						counter = blob_launch_counter;
						break;
					}
			}
		}	
	}
	
	public bool CheckSurrounded()
	{
		foreach (Vector2I offset in MapManager.Instance.surround_offsets[position[1] % MapManager.Instance.surround_offsets.Length])
		{
			if (MapManager.Instance.GetTile(offset + position).occupierBlob == null)
			{
				if (surrounded == true)
					SetSurrounded(false);
					
				return false;
			}
		}
		
		if (surrounded == false)
			SetSurrounded(true);
		
		return true;
	}
	
	public void SetSurrounded(bool is_surrounded)
	{
		surrounded = is_surrounded;
		if (is_surrounded && state == BlobAction.Grow)
		{
			state = BlobAction.Launch;
			if (preserve_counter_on_surround)
			{
				counter = blob_launch_counter - blob_grow_counter + counter;
			}
			else
			{
				counter = blob_launch_counter;
			}
		}
		else if (!is_surrounded && state == BlobAction.Launch)
		{
			state = BlobAction.Grow;
			if (preserve_counter_on_surround)
			{
				counter = blob_grow_counter - blob_launch_counter + counter;
			}
			else
			{
				counter = blob_grow_counter;
			}
		}
	}
	
	public BlobAction BlobTurn()
	{
		GD.Print($"Blob [{position} {surrounded}] Action({state}) Counter({counter})");
		
		if (frozen && --freeze_counter <= 0)
		{
			frozen = false;
			return BlobAction.Thaw;
		}	
		else if (--counter <= 0)
		{
			BlobAction action = state;
			
			if (surrounded)
			{
				state = BlobAction.Launch;
				counter = blob_launch_counter;
			}
			else
			{
				state = BlobAction.Grow;
				counter = blob_grow_counter;
			}
		
			return action;
		}
		
		return BlobAction.None;
	}
}
