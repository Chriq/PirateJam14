using Godot;
using System;

public partial class SceneChange : Node
{
	public void LoadMenu()
	{
		GetTree().ChangeSceneToFile("res://main.tscn");
	}
	
	public void LoadGame()
	{
		GetTree().ChangeSceneToFile("res://main.tscn");
	}
}
