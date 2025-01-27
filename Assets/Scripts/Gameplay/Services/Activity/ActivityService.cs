using System;
using Gameplay.Services.Activity.Base;
using Gameplay.Services.Base;
using Signals;
using Signals.Activities;
using Signals.Level;
using Zenject;

namespace Gameplay.Services.Activity
{
    public class ActivityService : GameService, IInitializable
    {
        [Inject] private ActivityServiceConfig _config;

        private ActivityConfig _currentActivity;

        private int _currentActivityIndex;

        private int _currentActivityRepeatCount;

        public override void Initialize()
        {
            _signalBus.Subscribe<TreeLevelChangedSignal>(ProvideActivity);
            _signalBus.Subscribe<ActivityPassedSignal>(OnActivityPassed);
            base.Initialize();
        }

        private void ProvideActivity()
        {
            _currentActivity = _config.activityConfigs[_currentActivityIndex];

            if (_config.activityConfigs[_currentActivityIndex].repeatCount == _currentActivityRepeatCount)
                NextActivity();

            _signalBus.Fire(_currentActivity.Request);

            ++_currentActivityRepeatCount;
        }

        private void OnActivityPassed() => _signalBus.Fire(new LevelPassedSignal { IsWin = true });

        private void NextActivity()
        {
            ++_currentActivityIndex;

            if (_currentActivityIndex == _config.activityConfigs.Count)
                _currentActivityIndex = 0;


            _currentActivity = _config.activityConfigs[_currentActivityIndex];
        }
    }
}