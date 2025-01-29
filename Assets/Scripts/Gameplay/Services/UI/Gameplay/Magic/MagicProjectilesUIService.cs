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

        private MagicProjectilesBarrel _magicProjectilesBarrel;

        public override void Initialize()
        {
            _signalBus.Subscribe<EndGameRequest>(() => _magicProjectilesBarrel.gameObject.SetActive(false));
            base.Initialize();
        }

        public override async void Boot()
        {
            await CreateMagicProjectilesView();
            base.Boot();
        }

        private async Task CreateMagicProjectilesView()
        {
            _magicProjectilesBarrel = (await Addressables.InstantiateAsync(_config.projectilesViewReference))
                .GetComponent<MagicProjectilesBarrel>();

            _magicProjectilesBarrel.Initialize();

            _signalBus.Fire(new MagicProjectilesUIViewInitialized{ View = _magicProjectilesBarrel });
        }

        public void Reset()
        {
            if (!_magicProjectilesBarrel)
                return;
            
            _magicProjectilesBarrel.gameObject.SetActive(true);
            _magicProjectilesBarrel.Reset();
        }
    }
}