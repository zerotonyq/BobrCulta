using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Core.Container;
using Gameplay.Core.TargetTracking.Provider;
using Gameplay.Services.Base;
using Gameplay.Services.Boss.Config;
using Signals;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.Services.Boss
{
    public class BossService : GameService, IInitializable 
    {
        private ComponentContainer _currentBoss;

        private int _currentBossIndex;

        private IList<BossDifficultyConfig.BossConfig> _bossesConfigs;
        
        private int _currentDifficultySectionIndex;

        [Inject] private BossDifficultyConfig _config;

        public override void Initialize()
        {
            _signalBus.Subscribe<NextLevelRequest>(SpawnNext);
            
            NextSection();
            
            base.Initialize();
        }
        
        private async void SpawnNext()
        {
            if (_currentBoss) Addressables.ReleaseInstance(_currentBoss.gameObject);

            if (_currentBossIndex >= _bossesConfigs.Count)
            {
                NextSection();
                return;
            }

            _currentBoss =
                (await Addressables.InstantiateAsync(_bossesConfigs[_currentBossIndex].bossReference))
                .GetComponent<ComponentContainer>();

            _currentBoss.GetComponent<AutomationMagicComponentBinder>()
                .SetAbilityIntervals(_bossesConfigs[_currentBossIndex].abilityIntervals);

            await _currentBoss.Initialize();

            _signalBus.Fire(new NextBossSignal { Boss = _currentBoss });

            TargetProvider.SetBoss(_currentBoss.transform);
        }
        
        private void NextSection()
        {
            if (_currentDifficultySectionIndex >= _config.sections.Count)
                _signalBus.Fire(new EndGameSignal() { Condition = true });

            _config.sections[_currentDifficultySectionIndex].GenerateBossConfigs();
            
            _bossesConfigs = _config.sections[_currentDifficultySectionIndex].bossesConfigs;
            
            ++_currentDifficultySectionIndex;
        }

        public void OnBossDefeated()
        {
            _signalBus.Fire<LevelPassedSignal>();
        }
    }
}