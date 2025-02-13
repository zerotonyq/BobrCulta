using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Core.Container;
using Gameplay.Core.Health;
using Gameplay.Core.TargetTracking.Provider;
using Gameplay.Services.Base;
using Gameplay.Services.Boss.Config;
using Signals;
using Signals.Activities.Base;
using Signals.Activities.Boss;
using Signals.Chapter;
using Signals.Level;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Reset;
using Zenject;

namespace Gameplay.Services.Boss
{
    public class BossService : GameService, IInitializable, IResetable
    {
        [Inject] private BossActivityConfig _config;

        private ComponentContainer _currentBoss;

        private int _currentBossIndex;

        private Vector3 _bossInitPosition;

        private IList<BossActivityConfig.BossConfig> _bossesConfigs;

        private int _currentBossSectionIndex;

        public override void Initialize()
        {
            _signalBus.Subscribe<IActivityRequest>(SpawnNextBoss);

            base.Initialize();
        }

        public void Reset()
        {
            if (_currentBoss) 
                Addressables.ReleaseInstance(_currentBoss.gameObject);
            
            _currentBossSectionIndex = 0;
            NextSection();
        }

        public override void Boot()
        {
            NextSection();
            base.Boot();
        }

        private async void SpawnNextBoss(IActivityRequest request)
        {
            if (request is not BossActivityRequest bossActivityRequest)
                return;

            if (_currentBossIndex == _bossesConfigs.Count)
                NextSection();

            if (_bossesConfigs == null)
                return;

            _currentBoss =
                (await Addressables.InstantiateAsync(_bossesConfigs[_currentBossIndex].BossReference))
                .GetComponent<ComponentContainer>();

            _currentBoss.GetComponent<AutomationMagicComponentBinder>()
                .SetAbilityIntervals(_bossesConfigs[_currentBossIndex].AbilityIntervals);

            await _currentBoss.Initialize();

            _currentBoss.GetComponent<HealthComponent>().Dead += OnBossDefeated;

            _currentBoss.transform.position = bossActivityRequest.TreeLevelChangedSignal.LevelPosition +
                                              Vector3.up * _currentBoss.GetComponent<Collider>().bounds.extents.y / 2;

            TargetProvider.SetBoss(_currentBoss.transform);

            ++_currentBossIndex;

            _signalBus.Fire(new BossObtainedSignal { Boss = _currentBoss });
        }

        private void NextSection()
        {
            if (_currentBossSectionIndex == _config.sections.Count)
            {
                _signalBus.Fire(new LevelPassedSignal() { PassedType = LevelPassedSignal.LevelPassedType.Win });
                _bossesConfigs = null;
                return;
            }

            var bossSectionTemp = new BossActivityConfig.BossSection(_config.sections[_currentBossSectionIndex]);

            bossSectionTemp.Generate();

            _bossesConfigs = bossSectionTemp.Configs;

            _currentBossIndex = 0;

            ++_currentBossSectionIndex;

            _signalBus.Fire<NextChapterRequest>();
        }

        private void OnBossDefeated()
        {
            _currentBoss.GetComponent<HealthComponent>().Dead -= OnBossDefeated;

            _signalBus.Fire(new LevelPassedSignal() { PassedType = LevelPassedSignal.LevelPassedType.Next });

            if (_currentBoss)
                Addressables.ReleaseInstance(_currentBoss.gameObject);
        }
    }
}