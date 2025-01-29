using Signals;
using Signals.Activities.Base;
using UnityEngine;

namespace Gameplay.Services.Activity.Base
{
    public abstract class ActivityConfig : ScriptableObject
    {
        public int repeatCount;

        private IActivityRequest _request;

        public abstract IActivityRequest ConstructRequest(TreeLevelChangedSignal signal);
    }
}