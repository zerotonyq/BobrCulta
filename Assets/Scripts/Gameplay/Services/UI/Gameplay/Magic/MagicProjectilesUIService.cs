using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.Services.Base;
using Gameplay.Services.UI.Gameplay.Magic.Config;
using Gameplay.Services.UI.Gameplay.Magic.Views;
using Signals;
using Signals.GameStates;
using UnityEngine.AddressableAssets;
using Utils.Reset;
using Zenject;

namespace Gameplay.Services.UI.Gameplay.Magic
{
    public class MagicProjectilesUIService : GameService, IInitializable, IResetable
    {
        [Inject] private MagicProjectilesUIServiceConfig _config;

        private MagicProjectilesUIView _magicProjectilesUIView;

        public override void Initialize()
        {
            _signalBus.Subscribe<EndGameRequest>(() => _magicProjectilesUIView.gameObject.SetActive(false));
            base.Initialize();
        }

        public override async void Boot()
        {
            await CreateMagicProjectilesView();
            base.Boot();
        }

        private async Task CreateMagicProjectilesView()
        {
            _magicProjectilesUIView = (await Addressables.InstantiateAsync(_config.projectilesViewReference))
                .GetComponent<MagicProjectilesUIView>();

            _magicProjectilesUIView.Initialize();

            _signalBus.Fire(new MagicProjectilesUIViewInitialized{ View = _magicProjectilesUIView });
        }

        public void Reset()
        {
            if (!_magicProjectilesUIView)
                return;
            
            _magicProjectilesUIView.gameObject.SetActive(true);
            _magicProjectilesUIView.Reset();
        }
    }
}