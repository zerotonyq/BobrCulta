using Signals.Activities.Base;
using UnityEngine;

namespace Gameplay.Services.LevelActivity.Config
{
    public abstract class ActivityConfig : ScriptableObject
    {
        public abstract ActivitySignal ConstructSignal();
    }
}