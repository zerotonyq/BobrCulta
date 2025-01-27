using Cysharp.Threading.Tasks;
using Gameplay.Services.Base;
using Gameplay.Services.UI.Menu.Config;
using Gameplay.Services.UI.Menu.Views;
using Signals.GameStates;
using Signals.Menu;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.Services.UI.Menu
{
    public class MenuUIService : GameService, IInitializable 
    {
        [Inject] private MenuUIManagerConfig _config;

        private MenuView _menuView;


        public override async void Initialize()
        {
            _signalBus.Subscribe<MenuRequestSignal>(() => ToggleView(true));
            
            _menuView = (await Addressables.InstantiateAsync(_config.menuCanvas)).GetComponent<MenuView>();
            
            Subscribe();

            ToggleView(false);
            
            base.Initialize();
        }

        private void Subscribe()
        {
            _menuView.startGameButton.onClick.AddListener(() =>
            {
                _signalBus.Fire<StartGameRequest>();
                ToggleView(false);
            });
            
            _menuView.exitGameButton.onClick.AddListener(Application.Quit);
        }

        private void ToggleView(bool i) => _menuView.gameObject.SetActive(i);
    }
}