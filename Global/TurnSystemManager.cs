using Godot;
using System;

public partial class TurnSystemManager : Node {
	public override void _Ready() {
		PlayerTurnManager.Instance.PlayerTurnEnded += StartBlobTurn;
		BlobTurnManager.Instance.BlobTurnEnded += StartPlayerTurn;
	}

	private void StartPlayerTurn() {
		PlayerResources.Instance.CollectResources();
	}

	private void StartBlobTurn() {
		BlobTurnManager.Instance.BlobTurn();
	}
}
