using Cysharp.Threading.Tasks;
using Gameplay.Services.Base;
using Gameplay.Services.UI.Gameplay.EndGame.Config;
using Gameplay.Services.UI.Gameplay.EndGame.Views;
using Signals.GameStates;
using Signals.Menu;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.Services.UI.Gameplay.EndGame
{
    public class EndGameUIService : GameService, IInitializable
    {
        [Inject] private EndGameUIServiceConfig _config;

        private EndGameUIView _view;

        public override void Initialize()
        {
            _signalBus.Subscribe<EndGameRequest>(ProcessEndGame);
            base.Initialize();
        }

        public override async void Boot()
        {
            _view = (await Addressables.InstantiateAsync(_config.endGameViewReference)).GetComponent<EndGameUIView>();
            
            _view.ExitToMenuButton.onClick.AddListener(() =>
            {
                _signalBus.Fire<MenuRequestSignal>();
                _view.gameObject.SetActive(false);
            });
            
            _view.gameObject.SetActive(false);
            base.Boot();
        }

        private void ProcessEndGame(EndGameRequest request)
        {
            _view.gameObject.SetActive(true);
            _view.SetEndText(request.IsWin ? _config.winText : _config.looseText);
        }
    }
}