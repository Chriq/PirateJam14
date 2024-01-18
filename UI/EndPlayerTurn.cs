using Godot;
using System;

public partial class EndPlayerTurn : TextureButton {
	public void OnClick() {
		GD.Print("End Turn");
		PlayerTurnManager.Instance.EndTurn();
	}
}
