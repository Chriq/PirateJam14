using Godot;
using System;

public partial class DefenseLaser : IActiveDefense
{
	protected override void Fire(Blob target)
	{
		BlobTurnManager.Instance.DeleteBlob(target);
		
		// TODO: Add Animation
	}
}
