using Godot;
using System.Collections.Generic;

public partial class IBuilding : Node2D {
	[Export] public Building buildingData;
	[Export] public int buildTimer = 4;
	
	private Sprite2D ConstructionSprite = null;
	
	public BuildingState status { get; private set; }
	
	private int currentBuild;
	private int currentHealth;

	public Dictionary<ResourceType, int> buildCost;
	public Dictionary<ResourceType, int> repairCost;
	
	public override void _Ready() {
		ConstructionSprite = new Sprite2D();
		ConstructionSprite.Texture = PlayerTurnManager.Instance.ConstructionSprite;
		
		currentHealth = buildingData.maxTurnsOfHealth;
		Repair();
	}
	
	public void Repair()
	{
		status = BuildingState.BUILDING;
		currentBuild = buildTimer;
		AddChild(ConstructionSprite);
	}
	
	public void Construct()
	{
		if (--currentBuild <= 0)
		{
			status = BuildingState.ACTIVE;
			RemoveChild(ConstructionSprite);
		}
	}
	
	public virtual void DamageHealth(Vector2I position, int damage = 1)
	{
		currentHealth -= damage;
		
		if (currentHealth <= 0)
		{
			switch(buildingData.type)
			{
				case (BuildingType.WALL):
				case (BuildingType.LANDMINE):
					{
						MapManager.Instance.DeleteBuilding(position);
						break;
					}
			}
			
			if (status == BuildingState.BUILDING)
				RemoveChild(ConstructionSprite);
			
			status = BuildingState.DESTROYED;
			// TODO: Spawn Destroyed Icon
		}
	}

	public Dictionary<ResourceType, int> GetBuildCost() {
		if(buildCost == null) {
			buildCost = new();
			for(int i = 0; i < buildingData.requiredResources.Count; i++) {
				if(i < buildingData.buildCosts.Length) buildCost.Add(buildingData.requiredResources[i], buildingData.buildCosts[i]);
			}
		}

		return buildCost;
	}

	public Dictionary<ResourceType, int> GetRepairCost() {
		if(repairCost == null) {
			repairCost = new();
			for(int i = 0; i < buildingData.requiredResources.Count; i++) {
				if(i < buildingData.repairCosts.Length) repairCost.Add(buildingData.requiredResources[i], buildingData.repairCosts[i]);
			}
		}

		return repairCost;
	}
}
