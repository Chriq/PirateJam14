using Godot;
using System;
using System.Collections.Generic;

public partial class BuildingPanel : Node2D {
	[Export] private Label electricityLabel;
	[Export] private Label woodLabel;
	[Export] private Label steelLabel;
	[Export] private Label oilLabel;

	[Export] private VBoxContainer labelContainer;

	[Export] private Node2D buildCardsContainer;
	[Export] private Button repairButton;

	public override void _Ready() {
		PlayerResources.Instance.ResourcesChanged += UpdateResourceUI;
		PlayerTurnManager.Instance.HexNodeSelected += UpdateBuildUI;
		PlayerTurnManager.Instance.HexNodeSelected += UpdateRepairUI;
		PlayerTurnManager.Instance.HexNodeSelected += UpdateNameUI;
		MapManager.Instance.BuildingAdded += ResetBuildPanel;
		repairButton.Pressed += Repair;
		UpdateResourceUI();
		ResetBuildPanel();
	}

	private void UpdateNameUI() {
		HexNode selectedHexNode = PlayerTurnManager.Instance.selectedHexNode;
		labelContainer.Show();
		if(selectedHexNode.occupierBlob != null) {
			labelContainer.GetChild<Label>(0).Text = "BLOB";
			labelContainer.GetChild<Label>(1).Text = "";
			labelContainer.GetChild<Label>(2).Text = "";
			labelContainer.GetChild<Label>(3).Text = "";
		} else if(selectedHexNode.occupierBuilding != null) {
			labelContainer.GetChild<Label>(0).Text = selectedHexNode.occupierBuilding.buildingData.type.ToString().Replace('_', ' ');
			labelContainer.GetChild<Label>(1).Text = selectedHexNode.occupierBuilding.buildingData.description;
			labelContainer.GetChild<Label>(2).Text = "Health: " + selectedHexNode.occupierBuilding.currentHealth + " / " + selectedHexNode.occupierBuilding.buildingData.maxTurnsOfHealth;
			labelContainer.GetChild<Label>(3).Text = "Electricity: " + (selectedHexNode.occupierBuilding.status == BuildingState.UNPOWERED ? $"NEEDS {selectedHexNode.occupierBuilding.buildingData.electricityCostPerTurn} POWER" : "STABLE");
		} else {
			labelContainer.Hide();
		}
	}

	private void UpdateResourceUI() {
		electricityLabel.Text = PlayerResources.Instance.playerResourceCounts[ResourceType.ELECTRICITY].ToString();
		woodLabel.Text = PlayerResources.Instance.playerResourceCounts[ResourceType.WOOD].ToString();
		steelLabel.Text = PlayerResources.Instance.playerResourceCounts[ResourceType.STEEL].ToString();
		oilLabel.Text = PlayerResources.Instance.playerResourceCounts[ResourceType.OIL].ToString();
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
		bool isTileRepairable = selectedHexNode.occupierBlob == null && 
								selectedHexNode.occupierBuilding?.currentHealth < selectedHexNode.occupierBuilding?.buildingData.maxTurnsOfHealth;
		bool canAfford = true;

		Dictionary<ResourceType, int> repairCosts = selectedHexNode.occupierBuilding?.GetRepairCost();
		if(repairCosts != null) {
			foreach(ResourceType resource in repairCosts?.Keys) {
				if(PlayerResources.Instance.playerResourceCounts[resource] < repairCosts[resource]) {
					canAfford = false;
				}
			}
		}
		

		if(isTileRepairable) {
			repairButton.Show();
			repairButton.Disabled = false;
		} else if(!canAfford) {
			repairButton.Disabled = true;
		} else {
			repairButton.Hide();
		}
	}

	public void Repair() {
		HexNode selectedHexNode = PlayerTurnManager.Instance.selectedHexNode;
		MapManager.Instance.RepairBuildingOnTile(selectedHexNode);
		UpdateRepairUI();
		UpdateResourceUI();
		UpdateNameUI();
	}

	public void ResetBuildPanel() {
		buildCardsContainer.Hide();
		repairButton.Hide();
		labelContainer.Hide();
	}

	public void EndTurn() {
		ResetBuildPanel();
		PlayerTurnManager.Instance.EndTurn();
	}
}
