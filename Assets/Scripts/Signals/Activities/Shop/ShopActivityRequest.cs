using Signals.Activities.Base;

namespace Signals.Activities.Shop
{
    public struct ShopActivityRequest : IActivityRequest
    {
        public TreeLevelChangedSignal TreeLevelChangedSignal { get; set; }
    }
}