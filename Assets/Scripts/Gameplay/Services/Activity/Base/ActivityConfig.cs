using Signals.Activities.Base;
using UnityEngine;

namespace Gameplay.Services.Activity.Base
{
    public abstract class ActivityConfig : ScriptableObject
    {
        public int repeatCount;
        
        public IActivitySignal Signal => _signal ??= ConstructSignal();

        private IActivitySignal _signal;

        protected abstract IActivitySignal ConstructSignal();
    }
}