using System.Collections.Generic;
using Gameplay.Services.Base;
using Signals.Activities.Base;
using Zenject;

namespace Gameplay.Services.LevelActivity
{
    public class LevelActivityService : GameService, IInitializable
    {
        [Inject] public List<ActivitySignal> _activitySignals = new();
        
        public void RequestNextActivity()
        {
            
        }
    }
}