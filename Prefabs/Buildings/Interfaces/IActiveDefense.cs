using Godot;
using System;

public abstract partial class IActiveDefense : IBuilding
{
	[Export] private int defense_fire_counter = 3;
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
			
			if (dist < target_distance)
			{
				// Prioritize unfrozen blobs
				if (!frozen_target && blob.freeze_counter > 0)
					continue;
				
				// Update current target
				target_distance = dist;
				frozen_target = blob.freeze_counter > 0;
				target = blob;
			}
		}
		
		//GD.Print($"ActiveDefense:GetTarget: {target} {target_distance} {frozen_target}");
		return target;
	}
	
	public void Action()
	{
		if (counter-- <= 0)
		{
			// TODO: Update from building dats
			counter = defense_fire_counter;
			
			Blob target = GetTarget();
			
			
			if (target != null)
			{
				Fire(target);
			}
		}
	}
}
