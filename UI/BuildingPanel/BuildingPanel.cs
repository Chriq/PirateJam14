using Godot;
using System;

public partial class BuildingPanel : Node2D {
	[Export] private Label electricityLabel;
	[Export] private Label woodLabel;
	[Export] private Label steelLabel;

    public override void _Ready() {
        PlayerResources.Instance.ResourcesCollected += UpdateResourceUI;
    }

    private void UpdateResourceUI() {
		electricityLabel.Text = PlayerResources.Instance.playerResourceCounts[ResourceType.ELECTRICITY].ToString();
		woodLabel.Text = PlayerResources.Instance.playerResourceCounts[ResourceType.WOOD].ToString();
		steelLabel.Text = PlayerResources.Instance.playerResourceCounts[ResourceType.STEEL].ToString();
	}
}
