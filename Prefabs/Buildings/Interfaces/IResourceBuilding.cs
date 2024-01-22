using System.Collections.Generic;
using Godot;

public abstract partial class IResourceBuilding : IBuilding {
	[Export] protected int yieldAmt = 5;
	public abstract Dictionary<ResourceType, int> YieldResources();
}
