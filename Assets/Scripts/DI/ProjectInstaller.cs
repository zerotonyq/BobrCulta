﻿using Signals;
using Signals.Activities;
using Signals.Activities.Boss;
using Signals.Activities.Shop;
using Signals.Boot;
using Signals.Chapter;
using Signals.Cutscenes;
using Signals.GameStates;
using Signals.Initialization;
using Signals.Level;
using Signals.Menu;
using Signals.Player;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
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

            Container.DeclareSignal<StartGameRequest>();
            Container.DeclareSignal<EndGameRequest>();

            Container.DeclareSignal<BossObtainedSignal>();

            Container.DeclareSignal<PlayerInitializedSignal>().RunAsync();

            Container.DeclareSignal<InitializedServiceSignal>();

            Container.DeclareSignal<BootRequest>().RunAsync();
            Container.DeclareSignal<ServiceBootEndRequest>();

            Container.DeclareSignal<NextLevelRequest>();
            Container.DeclareSignal<LevelPassedSignal>();
            Container.DeclareSignal<NextChapterRequest>();

            Container.DeclareSignal<MagicProjectilesUIViewInitialized>();

            Container.DeclareSignal<TreeLevelChangedSignal>();

            Container.DeclareSignalWithInterfaces<BossActivityRequest>();
            Container.DeclareSignalWithInterfaces<ShopActivityRequest>();

            Container.DeclareSignal<ActivityPassedSignal>();

            Container.DeclareSignal<MenuRequestSignal>();
            Container.DeclareSignal<CutsceneEndSignal>();
        }
    }
}