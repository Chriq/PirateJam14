using System.Collections.Generic;

public abstract partial class IResourceBuilding : IBuilding {
	public abstract Dictionary<ResourceType, int> YieldResources();
}
