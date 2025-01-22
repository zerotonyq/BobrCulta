using Signals.Activities.Base;
using UnityEngine;

namespace Gameplay.Services.LevelActivity.Base
{
    public abstract class ActivityConfig : ScriptableObject
    {
        public abstract ActivitySignal ConstructSignal();
    }
}