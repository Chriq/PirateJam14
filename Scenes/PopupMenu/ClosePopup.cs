using Godot;
using System;

public partial class ClosePopup : Button
{
	public void Close()
	{
		Node n = GetTree().Root.GetNode("MainMenu/Menu");
		SceneChange n_script = (SceneChange) n;
		n_script.ClosePopup();
	}
}
