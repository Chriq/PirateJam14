using Godot;
using System;

public partial class Wall : Node2D {
	[Export] private Building buildingData;
	private int currentHealth;

    public override void _Ready() {
        currentHealth = buildingData.maxTurnsOfHealth;
    }
}
