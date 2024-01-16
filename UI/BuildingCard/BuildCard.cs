using Godot;

public partial class BuildCard : Node {
	[Export] private Building buildingInfo;

	[Export] Sprite2D sprite;
	[Export] Label infoLabel;

	public override void _Ready() {
		//sprite.Texture = buildingInfo.sprite;
		
		string info = "Build: " + buildingInfo.buildCost;
		info += "\nRepair: " + buildingInfo.repairCost;
		infoLabel.Text = info;
	}
}
