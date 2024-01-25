using Godot;
using System;

public partial class Landmine : IBuilding {
	[Export] private int LandmineDamage = 1;
	
	public override void DamageHealth(Vector2I position, int damage = 1)
	{
		bool primed = status == BuildingState.ACTIVE;
		
		base.DamageHealth(position, damage);
		
		if (primed && status == BuildingState.DESTROYED)
			Detonate(position);
	}
	
	public void Detonate(Vector2I gridPosition) {
		foreach (Vector2I offset in MapManager.Instance.GetOffsets(gridPosition))
		{
			Vector2I pos = offset + gridPosition;
			
			HexNode node = MapManager.Instance.GetTile(pos);
			
			if (node.occupierBuilding != null)
			{
				node.occupierBuilding.DamageHealth(pos, LandmineDamage);
			}
			if (node.occupierBlob != null)
			{
				BlobTurnManager.Instance.DeleteBlob(node.occupierBlob);
			}
		}

		QueueFree();
	}
}
