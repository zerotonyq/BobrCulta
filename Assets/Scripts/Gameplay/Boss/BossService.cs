using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Boss.Config;
using Gameplay.Core.ComponentContainer;
using Gameplay.Core.TargetTracking.Provider;
using Signals;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.Boss
{
    public class BossService
    {
        private ComponentContainer _currentBoss;
        
        private IList<AssetReferenceGameObject> _bossesReferences;
        
        private SignalBus _signalBus;
        
        private int _bossIndex;

        [Inject]
        public void Initialize(BossServiceConfig config,  SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _bossesReferences = config.bossesReferences;
            
            _signalBus.Subscribe<NextBossRequestSignal>(SpawnNext);
        }

        private async void SpawnNext()
        {
            if (_currentBoss) Addressables.ReleaseInstance(_currentBoss.gameObject);

            _bossIndex = _bossIndex >= _bossesReferences.Count ? 0 : _bossIndex++;

            _currentBoss =
                (await Addressables.InstantiateAsync(_bossesReferences[_bossIndex])).GetComponent<ComponentContainer>();

            _signalBus.Fire(new NextBossSignal { Boss = _currentBoss });

            SetTargetForTargetProvider();
        }

        private void SetTargetForTargetProvider() => TargetProvider.SetTarget(_currentBoss.transform);
    }
}