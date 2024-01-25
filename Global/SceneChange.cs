using Godot;
using System;

public partial class SceneChange : Node
{
	Node sc = null;
	
	public void LoadMenu()
	{
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}
	
	public void LoadGame()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Game.tscn");
	}
	
	public void PopupInstructions()
	{
		if (sc != null)
			ClosePopup();
			
		sc = GD.Load<PackedScene>("res://Scenes/PopupMenu/Instructions.tscn").Instantiate();
		GD.Print(sc);
		GetTree().Root.AddChild(sc);
	}
	public void PopupCredits()
	{
		if (sc != null)
			ClosePopup();
			
		sc = GD.Load<PackedScene>("res://Scenes/PopupMenu/Credits.tscn").Instantiate();
		GD.Print(sc);
		GetTree().Root.AddChild(sc);
	}
	public void ClosePopup()
	{
		sc.QueueFree();
		sc = null;
	}
	
	public void ExitGame()
	{
		GetTree().Quit();
	}
}

