using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Core.Container;
using Gameplay.Core.TargetTracking.Provider;
using Gameplay.Services.Base;
using Gameplay.Services.Difficulty.Config;
using Signals;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.Services.Boss
{
    public class BossService :GameService, IInitializable 
    {
        private ComponentContainer _currentBoss;

        private int _currentBossIndex;

        private IList<DifficultyConfig.BossConfig> _bossesConfigs;

        [Inject] private SignalBus _signalBus;

        public override void Initialize()
        {
            _signalBus.Subscribe<NextBossRequestSignal>(SpawnNext);
            _signalBus.Subscribe<NextDifficultySectionSignal>(SetBossesConfigs);
            
            base.Initialize();
        }

        private void SetBossesConfigs(NextDifficultySectionSignal nextDifficultySectionSignal)
        {
            _bossesConfigs = nextDifficultySectionSignal.DifficultySection.bossesConfigs;
        }

        public async void SpawnNext()
        {
            if (_currentBoss) Addressables.ReleaseInstance(_currentBoss.gameObject);

            if (_currentBossIndex >= _bossesConfigs.Count)
            {
                _signalBus.Fire<NextDifficultySectionRequestSignal>();
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
    }
}