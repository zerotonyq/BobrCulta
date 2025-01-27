﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Core.Container;
using Gameplay.Core.Health;
using Gameplay.Core.TargetTracking.Provider;
using Gameplay.Services.Base;
using Gameplay.Services.Boss.Config;
using Signals;
using Signals.Activities;
using Signals.Activities.Base;
using Signals.Activities.Boss;
using Signals.Chapter;
using Signals.GameStates;
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
            _signalBus.Subscribe<TreeLevelChangedSignal>(SetBossInitPosition);
            _signalBus.Subscribe<IActivityRequest>(SpawnNextBoss);

            base.Initialize();
        }


        public void Reset()
        {
            _currentBossSectionIndex = 0;
            NextSection();
        }

        public override void Boot()
        {
            NextSection();
            base.Boot();
        }
        
        private void SetBossInitPosition(TreeLevelChangedSignal signal) => _bossInitPosition = signal.LevelPosition;

        private async void SpawnNextBoss(IActivityRequest request)
        {
            if (request is not BossActivityRequest)
                return;
            
            if(_currentBossIndex == _bossesConfigs.Count)
                NextSection();

            if (_bossesConfigs == null)
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

            TargetProvider.SetBoss(_currentBoss.transform);

  
            
            ++_currentBossIndex;
            
            _signalBus.Fire(new BossObtainedSignal { Boss = _currentBoss });
        }

        private void NextSection()
        {
            if (_currentBossSectionIndex == _config.sections.Count)
            {
                _signalBus.Fire(new EndGameRequest{IsWin = true});
                _bossesConfigs = null;
                return;
            }
            
            var bossSectionTemp = new BossActivityConfig.BossSection(_config.sections[_currentBossSectionIndex]);
            
            bossSectionTemp.Generate();

            _bossesConfigs = bossSectionTemp.configs;

            _currentBossIndex = 0;

            ++_currentBossSectionIndex;
            
            _signalBus.Fire<NextChapterRequest>();
        }

        private void OnBossDefeated()
        {
            _currentBoss.GetComponent<HealthComponent>().Dead -= OnBossDefeated;
            _signalBus.Fire<ActivityPassedSignal>();
        }
    }
}