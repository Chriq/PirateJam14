using Godot;
using System;

public abstract partial class IActiveDefense : IBuilding
{
	[Export] private int defense_fire_counter = 1;
	private int counter;
	
	public override void _Ready() {
		base._Ready();
		
		counter = defense_fire_counter;
	}
	
	protected abstract void Fire(Blob target);
	
	protected Blob GetTarget()
	{
		float target_distance = float.PositiveInfinity;
		bool frozen_target = true;
		Blob target = null;
		
		foreach (Blob blob in BlobTurnManager.Instance.Blobs)
		{
			float dist = GlobalPosition.DistanceTo(blob.GlobalPosition);
			
			// Prioritize unfrozen blobs
			if (!frozen_target && blob.freeze_counter > 0)
				continue;
			
			// Target Closest Unfrozen
			if (dist < target_distance || frozen_target && blob.freeze_counter <= 0)
			{
				// Update current target
				target_distance = dist;
				frozen_target = blob.freeze_counter > 0;
				target = blob;
			}
		}
		
		return target;
	}
	
	public void Action()
	{
		if (counter-- <= 0)
		{
			counter = defense_fire_counter;
			
			Blob target = GetTarget();
			
			if (target != null)
			{
				Fire(target);
			}
		}
	}
}
