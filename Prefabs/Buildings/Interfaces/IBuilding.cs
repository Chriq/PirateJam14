using Godot;
using System.Collections.Generic;

public partial class IBuilding : Node2D {
	[Export] public Building buildingData;
	
	private Sprite2D ConstructionSprite = null;
	
	public BuildingState status;
	
	[Export] public int currentBuild = 10;
	public int currentHealth;

	public Dictionary<ResourceType, int> buildCost;
	public Dictionary<ResourceType, int> repairCost;
	

	private Sprite2D damagedSprite;
	
	public override void _Ready() {
		ConstructionSprite = new Sprite2D();
		ConstructionSprite.Texture = PlayerTurnManager.Instance.ConstructionSprite;

		damagedSprite = new();
		AddChild(damagedSprite);
		
		currentHealth = buildingData.maxTurnsOfHealth;
		
		if (currentBuild > 0)
			Repair();
	}
	
	public void Repair()
	{		
		status = BuildingState.BUILDING;
		currentBuild = buildingData.buildTimer;
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
		if(currentHealth < 0) {
			currentHealth = 0;
		}

		if(currentHealth > 0) {
			damagedSprite.Texture = buildingData.damageSprite;
		} else {
			damagedSprite.Texture = buildingData.destroyedSprite;
		}
		
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

	public void UpdateDamageSprite() {
		if(damagedSprite != null) {
			if(status.Equals(BuildingState.UNPOWERED)) {
				damagedSprite.Texture = buildingData.needsElectricitySprite;
			} else if(currentHealth == buildingData.maxTurnsOfHealth) {
				damagedSprite.Texture = null;
			} else {
				damagedSprite.Texture = buildingData.damageSprite;
			}
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
