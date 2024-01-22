using System.Collections.Generic;

public partial class OilRefinery : IResourceBuilding {
	public override Dictionary<ResourceType, int> YieldResources() {
		Dictionary<ResourceType, int> yields = new Dictionary<ResourceType, int> {
			{ ResourceType.OIL, yieldAmt }
		};

		return yields;
	}
}
