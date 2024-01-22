using Godot;
using System;

public partial class EndPlayerTurn : Button {
	public void OnClick() {
		GD.Print("End Turn");
		PlayerTurnManager.Instance.EndTurn();
	}
}
