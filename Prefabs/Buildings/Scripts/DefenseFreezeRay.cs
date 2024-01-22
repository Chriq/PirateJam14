using Godot;
using System;

public partial class DefenseFreezeRay : IActiveDefense
{
	protected override void Fire(Blob target)
	{	
		target.Freeze(BlobTurnManager.Instance.blob_freeze_counter);
		
		// TODO: Add Animation
	}
}
