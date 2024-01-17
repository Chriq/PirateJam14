using Godot;
using System;

public partial class TESTSCRIPTPLEASEWORK : Node2D
{	
	//private bool CheckRoad(int i)
	//{
		//if (grid_x % 2 == 1)
			//return
				//i % grid_x == grid_x / 2      || 
				//i / grid_x == grid_y / 2;
		//else
			//return
				//i % grid_x == grid_x / 2      ||
				//i % grid_x == (grid_x / 2) - 1  || 
				//i / grid_x == grid_y / 2      || 
				//i / grid_x == (grid_y / 2 - 1);
	//}
	//private bool CheckBound(int i)
	//{
		//double x = grid_x / 2 - 0.5 - i % grid_x;
		//double y = grid_y / 2 - 0.5 - i / grid_x;
		//
		//return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) > bound;
	//}
	
	
	//
	//unsafe public void GridInit()
	//{
		//int squares = grid_x * grid_y;
		//
		//grid = new GridSquare[squares];		
		//
		//for (int i = 0; i < squares; i++)
		//{
			//// Pass Conditions
			//if(CheckRoad(i))
				//continue;
			//if(CheckBound(i))
				//continue;
			//
			//// Select Random Building
			//PackedScene obj = BuildingScenes[GD.Randi() % BuildingScenes.Length];
			//
			//// Instantiate
			//Node2D newobj = (Node2D) obj.Instantiate();
			//newobj.Position = new Vector2(square_size * (i % grid_x), square_size * (i / grid_x));
			//AddChild(newobj);
		//}
	//}
}
