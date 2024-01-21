using System.Collections.Generic;

public partial class PowerPlant : IResourceBuilding {
	public override Dictionary<ResourceType, int> YieldResources() {
		Dictionary<ResourceType, int> yields = new Dictionary<ResourceType, int> {
			{ ResourceType.ELECTRICITY, 5 }
		};

		return yields;
	}
}
