using Godot;
using System.Collections.Generic;

public partial class IBuilding : Node2D {
	[Export] public Building buildingData;
	[Export] public int buildTimer = 4;
	
	public BuildingState status { get; private set; }
	
	private int currentBuild;
	private int currentHealth;
	
	public override void _Ready() {
		currentHealth = buildingData.maxTurnsOfHealth;
		Repair();
	}
	
	public void Repair()
	{
		status = BuildingState.BUILDING;
		currentBuild = buildTimer;
	}
	
	public void Construct()
	{
		if (--currentBuild <= 0)
			status = BuildingState.ACTIVE;
	}
	
	public void DamageHealth(int damage = 1)
	{
		currentHealth -= damage;
		
		if (currentHealth <= 0)
			status = BuildingState.DESTROYED;
	}
}
