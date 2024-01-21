using Godot;
using System;

public partial class DefenseLaser : IActiveDefense
{
	protected override void Fire()
	{
		float target_distance = float.PositiveInfinity;
		Blob target = null;
		
		foreach (Blob blob in BlobTurnManager.Instance.Blobs)
		{
			float dist = GlobalPosition.DistanceTo(blob.GlobalPosition);
			
			if (dist < target_distance)
			{
				target_distance = dist;
				target = blob;
			}
		}

		BlobTurnManager.Instance.DeleteBlob(target);
	}
}
