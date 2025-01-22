using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.Services.Base;
using Gameplay.Services.UI.Magic.Config;
using Gameplay.Services.UI.Magic.Views;
using Signals;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.Services.UI.Magic
{
    public class MagicProjectilesUIService : GameService, IInitializable
    {
        [Inject] private MagicProjectilesUIServiceConfig _config;

        private MagicProjectilesUIView _magicProjectilesUIView;

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

        public void ToggleView(bool i)
        {
            if (!_magicProjectilesUIView)
                return;

            _magicProjectilesUIView.gameObject.SetActive(i);
        }
    }
}