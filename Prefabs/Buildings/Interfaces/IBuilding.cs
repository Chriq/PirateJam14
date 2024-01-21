using Godot;
using System.Collections.Generic;

public partial class IBuilding : Node2D {
	[Export] public Building buildingData;
	[Export] public int buildTimer = 4;
	
	private Sprite2D ConstructionSprite = null;
	
	public BuildingState status { get; private set; }
	
	private int currentBuild;
	private int currentHealth;
	
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
	
	public void DamageHealth(int damage = 1)
	{
		currentHealth -= damage;
		
		if (currentHealth <= 0)
		{
			status = BuildingState.DESTROYED;
		}
	}
}
