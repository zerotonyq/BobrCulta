using Gameplay.Services.Boss;
using Gameplay.Services.Camera;
using Gameplay.Services.Camera.Config;
using Gameplay.Services.Difficulty;
using Gameplay.Services.Difficulty.Config;
using Gameplay.Services.Level;
using Gameplay.Services.Level.Config;
using Gameplay.Services.Player;
using Gameplay.Services.Player.Config;
using Gameplay.Services.UI.Magic;
using Gameplay.Services.UI.Magic.Binders;
using Gameplay.Services.UI.Magic.Config;
using Gameplay.Services.UI.Menu;
using Gameplay.Services.UI.Menu.Config;
using GameState;
using UnityEngine;
using Utils.Disposing;
using Zenject;

namespace DI
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private DisposeManager disposeManager;
        
        [SerializeField] private LevelConfig levelConfig;
        [SerializeField] private CameraConfig cameraConfig;
        [SerializeField] private DifficultyConfig difficultyConfig;
        [SerializeField] private MenuUIManagerConfig menuUIManagerConfig;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private MagicProjectilesUIServiceConfig magicProjectilesUIServiceConfig;

        public override void InstallBindings()
        {
            BindExecutionOrders();
            
            Container.BindInstance(disposeManager);

            Container.BindInstance(levelConfig);
            Container.BindInstance(cameraConfig);
            Container.BindInstance(difficultyConfig);
            Container.BindInstance(menuUIManagerConfig);
            Container.BindInstance(playerConfig);
            Container.BindInstance(magicProjectilesUIServiceConfig);

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<DifficultyService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<BossService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<LevelService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<MagicProjectilesUIService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<MagicProjectilesUIBinder>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<MenuUIService>().AsSingle();
        }

        private void BindExecutionOrders()
        {
            Container.BindExecutionOrder<GameStateMachine>(-1000);
            Container.BindExecutionOrder<DifficultyService>(-10);
            Container.BindExecutionOrder<BossService>(-20);
            Container.BindExecutionOrder<PlayerService>(-40);
            Container.BindExecutionOrder<MagicProjectilesUIService>(-39);
            Container.BindExecutionOrder<MagicProjectilesUIBinder>(-38);
            Container.BindExecutionOrder<LevelService>(-50);
            Container.BindExecutionOrder<CameraProvider>(-60);
            Container.BindExecutionOrder<MenuUIService>(-70);
        }
    }
}