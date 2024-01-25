using Godot;
using System;

public partial class SceneChange : Node
{
	public void LoadMenu()
	{
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}
	
	public void LoadGame()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Game.tscn");
	}
	
	public void ExitGame()
	{
		GetTree().Quit();
	}
}
