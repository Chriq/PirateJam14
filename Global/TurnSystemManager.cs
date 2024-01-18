using Godot;
using System;

public partial class TurnSystemManager : Node {
    public override void _Ready() {
        PlayerTurnManager.Instance.PlayerTurnEnded += StartBlobTurn;
		BlobTurnManager.Instance.BlobTurnEnded += StartPlayerTurn;
    }

	private void StartPlayerTurn() {
		
	}

	private void StartBlobTurn() {
		BlobTurnManager.Instance.BlobTurn();
	}
}
