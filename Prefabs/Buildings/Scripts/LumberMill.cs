using System.Collections.Generic;

public partial class LumberMill : IResourceBuilding {
	public override Dictionary<ResourceType, int> YieldResources() {
		Dictionary<ResourceType, int> yields = new Dictionary<ResourceType, int> {
			{ ResourceType.WOOD, yieldAmt }
		};

		return yields;
	}
}
