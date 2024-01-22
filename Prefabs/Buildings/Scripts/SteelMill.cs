using System.Collections.Generic;

public partial class SteelMill : IResourceBuilding {
	public override Dictionary<ResourceType, int> YieldResources() {
		Dictionary<ResourceType, int> yields = new Dictionary<ResourceType, int> {
			{ ResourceType.STEEL, yieldAmt }
		};

		return yields;
	}
}
