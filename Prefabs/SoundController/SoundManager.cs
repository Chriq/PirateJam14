using Godot;
using System;
using System.Collections.Generic;

public partial class SoundManager : Node {
	[Export] private AudioStream mainTheme;
	[Export] private AudioStreamPlayer backgroundMusicPlayer;

	public static SoundManager Instance;

	public override void _Ready() {
		Instance = this;
		backgroundMusicPlayer.Stream = mainTheme;
	}

	public override void _ExitTree()
	{
		Instance = null;
	}

	public void PlayBackgroundMusic() {
		backgroundMusicPlayer.Stream = mainTheme;
		backgroundMusicPlayer.VolumeDb = -25f;

		Tween tween = GetTree().CreateTween();
		backgroundMusicPlayer.Play();
		tween.TweenProperty(backgroundMusicPlayer, "volume_db", 0f, 0.5f);
	}
}
