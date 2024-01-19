using Godot;
using System.Collections.Generic;

public abstract partial class IBuilding : Node2D {
	[Export] private Building buildingData;
	public int currentHealth { get; private set; }

    public override void _Ready() {
        currentHealth = buildingData.maxTurnsOfHealth;
    }

	public abstract Dictionary<ResourceType, int> YieldResources();
}
