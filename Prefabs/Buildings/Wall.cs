using System.Collections.Generic;

public partial class Wall : IBuilding {
    public override Dictionary<ResourceType, int> YieldResources() {
        return new Dictionary<ResourceType, int>();
    }
}
