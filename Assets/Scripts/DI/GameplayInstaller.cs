using Gameplay;
using Gameplay.Boss;
using Gameplay.Boss.Location;
using Gameplay.Boss.Location.Config;
using Gameplay.Camera;
using Gameplay.Camera.Config;
using Gameplay.EntryPoint;
using Gameplay.EntryPoint.Config;
using Signals;
using Unity.Cinemachine;
using UnityEngine;
using Utils.Disposing;
using Zenject;

namespace DI
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayEntryPointConfig gameplayEntryPointConfig;
        [SerializeField] private BossServiceConfig bossServiceConfig;
        [SerializeField] private CameraConfig cameraConfig;

        [SerializeField] private DisposeManager disposeManager;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameplayEntryPointConfig);
            Container.BindInstance(bossServiceConfig);
            Container.BindInstance(cameraConfig);
            Container.BindInstance(disposeManager);
            
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BossService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle().NonLazy();
            
            DeclareSignals();
        }


        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<NextBossSignal>();
            Container.DeclareSignal<NextBossRequestSignal>().RunAsync();
            Container.DeclareSignal<PlayerInitializedSignal>();
        }
    }
}