using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Core.Container;
using Gameplay.Core.Health;
using Gameplay.Core.TargetTracking.Provider;
using Gameplay.Services.Base;
using Gameplay.Services.Boss.Config;
using Signals;
using Signals.Activities;
using Signals.Activities.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.Services.Boss
{
    public class BossService : GameService, IInitializable
    {
        [Inject] private BossActivityConfig _config;
        
        private ComponentContainer _currentBoss;

        private int _currentBossIndex;

        private Vector3 _bossInitPosition;

        private IList<BossActivityConfig.BossConfig> _bossesConfigs;

        private int _currentBossSectionIndex;

        public override void Initialize()
        {
            _signalBus.Subscribe<TreeLevelChangedSignal>(SetBossInitPosition);
            _signalBus.Subscribe<IActivitySignal>(SpawnNext);

            base.Initialize();
        }

        private void SetBossInitPosition(TreeLevelChangedSignal signal) => _bossInitPosition = signal.LevelPosition;

        public override void Boot()
        {
            _signalBus.Fire<NextChapterRequest>();
            NextSection();
            base.Boot();
        }

        private async void SpawnNext(IActivitySignal signal)
        {
            if (signal is not BossActivitySignal)
                return;
            
            if (_currentBoss) 
                Addressables.ReleaseInstance(_currentBoss.gameObject);

            _currentBoss =
                (await Addressables.InstantiateAsync(_bossesConfigs[_currentBossIndex].bossReference))
                .GetComponent<ComponentContainer>();

            _currentBoss.GetComponent<AutomationMagicComponentBinder>()
                .SetAbilityIntervals(_bossesConfigs[_currentBossIndex].abilityIntervals);

            await _currentBoss.Initialize();

            _currentBoss.GetComponent<HealthComponent>().Dead += OnBossDefeated;

            _currentBoss.transform.position = _bossInitPosition;

            _signalBus.Fire(new NextBossSignal { Boss = _currentBoss });

            TargetProvider.SetBoss(_currentBoss.transform);

            if(_currentBossIndex == _bossesConfigs.Count)
                NextSection();
            
            ++_currentBossIndex;
        }

        private void NextSection()
        {
            var bossSectionReference = _config.sections[_currentBossSectionIndex];

            var bossSectionTemp = new BossActivityConfig.BossSection(bossSectionReference);
            
            bossSectionTemp.Generate();

            _bossesConfigs = bossSectionTemp.configs;

            _currentBossIndex = 0;

            ++_currentBossSectionIndex;
        }

        private void OnBossDefeated()
        {
            _currentBoss.GetComponent<HealthComponent>().Dead -= OnBossDefeated;
            _signalBus.Fire<LevelPassedSignal>();
        }
    }
}