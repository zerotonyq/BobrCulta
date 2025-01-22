using Cysharp.Threading.Tasks;
using Gameplay.Services.Base;
using Gameplay.Services.UI.Menu.Config;
using Gameplay.Services.UI.Menu.Views;
using Signals;
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
            _menuView = (await Addressables.InstantiateAsync(_config.menuCanvas)).GetComponent<MenuView>();
            
            Subscribe();
            
            base.Initialize();
        }

        private void Subscribe()
        {
            _menuView.startGameButton.onClick.AddListener(() =>
            {
                _signalBus.Fire<StartGameRequest>();
                ToggleView(false);
            });
        }

        public void ToggleView(bool i) => _menuView.gameObject.SetActive(i);
    }
}