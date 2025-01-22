using Gameplay.Services.Boss;
using Gameplay.Services.Boss.Config;
using Gameplay.Services.Camera;
using Gameplay.Services.Camera.Config;
using Gameplay.Services.Player;
using Gameplay.Services.Player.Config;
using Gameplay.Services.Tree;
using Gameplay.Services.Tree.Config;
using Gameplay.Services.UI.Magic;
using Gameplay.Services.UI.Magic.Binders;
using Gameplay.Services.UI.Magic.Config;
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
        [SerializeField] private BossDifficultyConfig bossDifficultyConfig;
        [SerializeField] private MenuUIManagerConfig menuUIManagerConfig;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private MagicProjectilesUIServiceConfig magicProjectilesUIServiceConfig;
        [SerializeField] private TreeServiceConfig treeServiceConfig;

        public override void InstallBindings()
        {
            BindExecutionOrders();

            Container.BindInstance(cameraConfig);
            Container.BindInstance(bossDifficultyConfig);
            Container.BindInstance(menuUIManagerConfig);
            Container.BindInstance(playerConfig);
            Container.BindInstance(magicProjectilesUIServiceConfig);
            Container.BindInstance(treeServiceConfig);

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

            Container.BindInterfacesAndSelfTo<BossService>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();

            Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();

            Container.BindInterfacesAndSelfTo<MagicProjectilesUIService>().AsSingle();

            Container.BindInterfacesAndSelfTo<MagicProjectilesUIBinder>().AsSingle();

            Container.BindInterfacesAndSelfTo<MenuUIService>().AsSingle();

            Container.BindInterfacesAndSelfTo<TreeService>().AsSingle();
        }

        private void BindExecutionOrders()
        {
            Container.BindExecutionOrder<GameStateMachine>(-1000);
            Container.BindExecutionOrder<BossService>(-20);
            Container.BindExecutionOrder<PlayerService>(-40);
            Container.BindExecutionOrder<MagicProjectilesUIService>(-39);
            Container.BindExecutionOrder<MagicProjectilesUIBinder>(-38);
            Container.BindExecutionOrder<CameraProvider>(-60);
            Container.BindExecutionOrder<MenuUIService>(-70);
        }
    }
}