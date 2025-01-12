using Gameplay.Magic;
using Gameplay.Services.Base;
using Gameplay.Services.UI.Magic.Views;
using Signals;
using Zenject;

namespace Gameplay.Services.UI.Magic.Binders
{
    public class MagicProjectilesUIBinder : GameService, IInitializable
    {
        [Inject] private SignalBus _signalBus;

        private MagicComponent _magicComponent;
        private MagicProjectilesUIView _magicProjectilesUIView;
        
        public override void Initialize()
        {
            _signalBus.Subscribe<PlayerInitializedSignal>(OnMagicComponentObtained);
            _signalBus.Subscribe<MagicProjectilesUIViewInitialized>(OnMagicProjectilesUIViewObtained);
            base.Initialize();
        }

        private void OnMagicProjectilesUIViewObtained(MagicProjectilesUIViewInitialized obj)
        {
            _magicProjectilesUIView = obj.View;
            CheckComponents();
        }

        private void OnMagicComponentObtained(PlayerInitializedSignal obj)
        {
            _magicComponent = obj.Player.GetComponent<MagicComponent>();
            CheckComponents();
        }

        private void CheckComponents()
        {
            if (!_magicComponent || !_magicProjectilesUIView)
                return;
            
            Bind(_magicComponent, _magicProjectilesUIView);
        }


        private void Bind(MagicComponent magicComponent, MagicProjectilesUIView magicProjectilesUIView)
        {
            magicComponent.MagicPickupableProvided += magicProjectilesUIView.OnMagicProjectileProvided;

            magicProjectilesUIView.MagicTypeProvided += magicComponent.FireProjectile;
            magicProjectilesUIView.MagicTypeRemoved += magicComponent.RemoveProjectile;
        }
    }
}