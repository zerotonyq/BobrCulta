using Gameplay.Services.Activity;
using Gameplay.Services.Activity.Base;
using Gameplay.Services.Boss;
using Gameplay.Services.Boss.Config;
using Gameplay.Services.Boxes.Emitter;
using Gameplay.Services.Boxes.Emitter.Config;
using Gameplay.Services.Camera;
using Gameplay.Services.Camera.Config;
using Gameplay.Services.DeadZone;
using Gameplay.Services.DeadZone.Config;
using Gameplay.Services.Player;
using Gameplay.Services.Player.Config;
using Gameplay.Services.Pool;
using Gameplay.Services.PoolablesRegistration.Config;
using Gameplay.Services.Shop;
using Gameplay.Services.Shop.Config;
using Gameplay.Services.Tree;
using Gameplay.Services.Tree.Config;
using Gameplay.Services.UI.Gameplay.EndGame;
using Gameplay.Services.UI.Gameplay.EndGame.Config;
using Gameplay.Services.UI.Menu;
using Gameplay.Services.UI.Menu.Config;
using GameState;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CameraConfig cameraConfig;

        [Space, Header("Activities")] 
        
        [SerializeField] private BossActivityConfig bossActivityConfig;
        [SerializeField] private ShopActivityConfig shopActivityConfig;

        [Space] [SerializeField] private MenuUIManagerConfig menuUIManagerConfig;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private TreeServiceConfig treeServiceConfig;
        [SerializeField] private BoxEmitterConfig boxEmitterConfig;
        [SerializeField] private ActivityServiceConfig activityServiceConfig;
        [SerializeField] private DeadZoneConfig deadZoneConfig;
        [SerializeField] private EndGameUIServiceConfig endGameUIServiceConfig;
        [SerializeField] private PoolableRegistrationServiceConfig poolableRegistrationServiceConfig;

        public override void InstallBindings()
        {
            BindExecutionOrders();

            Container.BindInstance(cameraConfig);
            Container.BindInstance(bossActivityConfig);
            Container.BindInstance(menuUIManagerConfig);
            Container.BindInstance(playerConfig);
            Container.BindInstance(treeServiceConfig);
            Container.BindInstance(boxEmitterConfig);
            Container.BindInstance(activityServiceConfig);
            Container.BindInstance(deadZoneConfig);
            Container.BindInstance(endGameUIServiceConfig);
            Container.BindInstance(poolableRegistrationServiceConfig);
            Container.BindInstance(shopActivityConfig);

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

            Container.BindInterfacesAndSelfTo<BossService>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();

            Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();

            Container.BindInterfacesAndSelfTo<MenuUIService>().AsSingle();

            Container.BindInterfacesAndSelfTo<TreeService>().AsSingle();

            Container.BindInterfacesAndSelfTo<BoxEmitter>().AsSingle();

            Container.BindInterfacesAndSelfTo<ActivityService>().AsSingle();

            Container.BindInterfacesAndSelfTo<DeadZoneService>().AsSingle();

            Container.BindInterfacesAndSelfTo<EndGameUIService>().AsSingle();

            Container.BindInterfacesAndSelfTo<PoolableRegistrationService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ShopService>().AsSingle();
        }

        private void BindExecutionOrders()
        {
            Container.BindExecutionOrder<GameStateMachine>(-1000);
            Container.BindExecutionOrder<BossService>(-20);
            Container.BindExecutionOrder<PlayerService>(-40);
            Container.BindExecutionOrder<CameraProvider>(-60);
            Container.BindExecutionOrder<MenuUIService>(-70);
        }
    }
}