using Godot;
using System.Collections.Generic;

public partial class IBuilding : Node2D {
	[Export] public Building buildingData;
	public int currentHealth { get; private set; }

    public override void _Ready() {
        currentHealth = buildingData.maxTurnsOfHealth;
    }
}
