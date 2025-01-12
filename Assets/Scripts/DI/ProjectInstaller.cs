using GameState;
using Signals;
using UnityEngine.AddressableAssets;
using Zenject;

namespace DI
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Addressables.InitializeAsync();
            DeclareSignals();
        }
        
        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<AllowedPickupablesSignal>();
            
            Container.DeclareSignal<EndGameSignal>();
            Container.DeclareSignal<StartGameRequest>();
            
            Container.DeclareSignal<NextBossRequestSignal>().RunAsync();
            Container.DeclareSignal<NextBossSignal>();
            
            Container.DeclareSignal<NextDifficultySectionRequestSignal>();
            Container.DeclareSignal<NextDifficultySectionSignal>();
            
            Container.DeclareSignal<PlayerInitializedSignal>();
            Container.DeclareSignal<InitializedServiceSignal>();
            
            Container.DeclareSignal<NextLevelRequest>();
            Container.DeclareSignal<ExitLevelRequest>();
            
            Container.DeclareSignal<BootRequest>().RunAsync();
            Container.DeclareSignal<ServiceBootEndRequest>();
            
            Container.DeclareSignal<MagicProjectilesUIViewInitialized>();
        }
    }
}