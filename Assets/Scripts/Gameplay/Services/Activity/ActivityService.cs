using Gameplay.Services.Activity.Base;
using Gameplay.Services.Base;
using Signals;
using Signals.Level;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.Activity
{
    public class ActivityService : GameService, IInitializable
    {
        [Inject] private ActivityServiceConfig _config;

        private ActivityConfig _currentActivity;

        private int _currentActivityIndex = -1;

        private int _currentActivityRepeatCount;

        public override void Initialize()
        {
            _signalBus.Subscribe<TreeLevelChangedSignal>(ProvideActivity);
            
            NextActivity();
            
            base.Initialize();
        }

        private void ProvideActivity(TreeLevelChangedSignal signal)
        {
            if (_config.activityConfigs[_currentActivityIndex].repeatCount == _currentActivityRepeatCount)
                NextActivity();

            _signalBus.Fire(_currentActivity.ConstructRequest(signal));

            ++_currentActivityRepeatCount;
        }

        private void NextActivity()
        {
            ++_currentActivityIndex;
            
            if (_currentActivityIndex == _config.activityConfigs.Count)
                _currentActivityIndex = 0;
            
            _currentActivity = _config.activityConfigs[_currentActivityIndex];
            _currentActivityRepeatCount = 0;
        }
    }
}