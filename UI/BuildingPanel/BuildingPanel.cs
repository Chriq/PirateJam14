using Godot;
using System;

public partial class BuildingPanel : Node2D {
	[Export] private Label electricityLabel;
	[Export] private Label woodLabel;
	[Export] private Label steelLabel;

	[Export] private Node2D buildCardsContainer;
	[Export] private TextureButton repairButton;

    public override void _Ready() {
        PlayerResources.Instance.ResourcesChanged += UpdateResourceUI;
		PlayerTurnManager.Instance.HexNodeSelected += UpdateBuildUI;
		PlayerTurnManager.Instance.HexNodeSelected += UpdateRepairUI;
		UpdateResourceUI();
    }

    private void UpdateResourceUI() {
		electricityLabel.Text = PlayerResources.Instance.playerResourceCounts[ResourceType.ELECTRICITY].ToString();
		woodLabel.Text = PlayerResources.Instance.playerResourceCounts[ResourceType.WOOD].ToString();
		steelLabel.Text = PlayerResources.Instance.playerResourceCounts[ResourceType.STEEL].ToString();
	}

	private void UpdateBuildUI() {
		HexNode selectedHexNode = PlayerTurnManager.Instance.selectedHexNode;
		bool isTileBuildable = selectedHexNode.Equals(default(HexNode));

		if(isTileBuildable) {
			buildCardsContainer.Show();
			foreach(BuildCard card in buildCardsContainer.GetChildren()) {
				card.Disabled = !PlayerResources.Instance.CanAffordBuilding(card.buildingInfo);
			}
		} else {
			buildCardsContainer.Hide();
		}
	}

	private void UpdateRepairUI() {
		HexNode selectedHexNode = PlayerTurnManager.Instance.selectedHexNode;
		bool isTileRepairable = selectedHexNode.occupierBuilding?.currentHealth < selectedHexNode.occupierBuilding?.buildingData.maxTurnsOfHealth;

		if(isTileRepairable) {
			repairButton.Show();
		} else {
			repairButton.Hide();
		}
	}

	public void Repair() {
		HexNode selectedHexNode = PlayerTurnManager.Instance.selectedHexNode;
		MapManager.Instance.RepairBuildingOnTile(selectedHexNode);
		UpdateRepairUI();
	}
}
