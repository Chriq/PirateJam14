using Godot;
using System;

public abstract partial class IActiveDefense : IBuilding
{
	[Export] private int defense_fire_counter = 3;
	private int counter;
	
	public override void _Ready() {
		base._Ready();
		
		counter = defense_fire_counter;
	}
	
	protected abstract void Fire();
	
	public void Action()
	{
		if (counter-- <= 0)
		{
			counter = defense_fire_counter;
			
			Fire();
		}
	}
}
