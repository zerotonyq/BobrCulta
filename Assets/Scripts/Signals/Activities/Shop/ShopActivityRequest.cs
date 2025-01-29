using Signals.Activities.Base;

namespace Signals.Activities
{
    public struct ShopActivityRequest : IActivityRequest
    {
        public TreeLevelChangedSignal TreeLevelChangedSignal { get; set; }
    }
}