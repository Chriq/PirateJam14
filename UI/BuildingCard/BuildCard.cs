using Godot;

public partial class BuildCard : TextureButton {
	[Export] public Building buildingInfo;
	[Export] private PackedScene buildingPrefab;

	[Export] Sprite2D sprite;
	[Export] Label infoLabel;

	public override void _Ready() {
		//sprite.Texture = buildingInfo.sprite;
		/*
		string info = "Build: " + buildingInfo.buildCost;
		info += "\nRepair: " + buildingInfo.repairCost;
		infoLabel.Text = info;*/

		Pressed += OnClick;
	}

	public void OnClick() {
		HexNode selectedHexNode = PlayerTurnManager.Instance.selectedHexNode;
		MapManager.Instance.BuildOnTile(selectedHexNode, buildingPrefab);

		//PlayerTurnManager.Instance.SelectItemToBuild(buildingPrefab);
	}
}
