using Signals.Activities.Base;
using UnityEngine;

namespace Gameplay.Services.Activity.Base
{
    public abstract class ActivityConfig : ScriptableObject
    {
        public int repeatCount;
        
        public IActivityRequest Request => _request ??= ConstructSignal();

        private IActivityRequest _request;

        protected abstract IActivityRequest ConstructSignal();
    }
}