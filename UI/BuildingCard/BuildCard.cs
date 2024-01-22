using System.Collections.Generic;
using Godot;

public partial class BuildCard : Button {
	[Export] public Building buildingInfo;
	[Export] private PackedScene buildingPrefab;

	[Export] Label nameLabel;
	[Export] Label descriptionLabel;

	[Export] Label electricLabel;
	[Export] Label woodLabel;
	[Export] Label steelLabel;
	[Export] Label oilLabel;

	public override void _Ready() {
		nameLabel.Text = buildingInfo.type.ToString();
		descriptionLabel.Text = buildingInfo.description;

		Dictionary<ResourceType, int> costs = buildingInfo.GetBuildCost();
		electricLabel.Text = buildingInfo.electricityCostPerTurn.ToString();
		woodLabel.Text = costs.ContainsKey(ResourceType.WOOD) ? costs[ResourceType.WOOD].ToString() : "0";
		steelLabel.Text = costs.ContainsKey(ResourceType.STEEL) ? costs[ResourceType.STEEL].ToString() : "0";
		oilLabel.Text = costs.ContainsKey(ResourceType.OIL) ? costs[ResourceType.OIL].ToString() : "0";

		Pressed += OnClick;
	}

	public void OnClick() {
		HexNode selectedHexNode = PlayerTurnManager.Instance.selectedHexNode;
		MapManager.Instance.BuildOnTile(selectedHexNode, buildingPrefab);

		//PlayerTurnManager.Instance.SelectItemToBuild(buildingPrefab);
	}
}
