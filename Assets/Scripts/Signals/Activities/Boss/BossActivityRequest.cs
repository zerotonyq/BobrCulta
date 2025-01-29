using Signals.Activities.Base;

namespace Signals.Activities.Boss
{
    public struct BossActivityRequest : IActivityRequest
    {
        public TreeLevelChangedSignal TreeLevelChangedSignal { get; set; }
    }
}