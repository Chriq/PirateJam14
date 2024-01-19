using System.Collections.Generic;

public partial class PowerPlant : IBuilding {
	public override Dictionary<ResourceType, int> YieldResources() {
        Dictionary<ResourceType, int> yields = new Dictionary<ResourceType, int> {
			{ ResourceType.ELECTRICITY, 5 }
		};

		return yields;
    }
}
