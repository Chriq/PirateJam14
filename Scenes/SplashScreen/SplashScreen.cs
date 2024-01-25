using Godot;
using System;

public partial class SplashScreen : Node
{
	[Export] private CanvasItem splashScreen;
	[Export] private FadeController fade;
	[Export] private PackedScene scene;
	[Export] Timer timer;
	[Export] bool enableMouse = false;

	public override async void _Ready()
	{
		await ToSignal(GetTree().CreateTimer(1f), "timeout");
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		fade.FadeOutWithCallback(splashScreen, delegate {
			timer.Start();
		});
	}

	public void FadeExitOut()
	{
		if(enableMouse) {
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}
	
	public void ContinueToGame() {
		fade.FadeInWithCallback(splashScreen, delegate {
			if(enableMouse) {
				Input.MouseMode = Input.MouseModeEnum.Visible;
			}
			GetTree().ChangeSceneToPacked(scene);
		});
	}
}
