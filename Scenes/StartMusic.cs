using Godot;
using System;

public partial class StartMusic : Node {
	public override void _Ready() {
		SoundManager.Instance.PlayBackgroundMusic();
	}

}
