using Gameplay.Magic.Abilities;
using Gameplay.Services.Base;
using Gameplay.Services.UI.Gameplay.Magic.Views;
using Signals;
using Signals.Player;
using Zenject;

namespace Gameplay.Services.UI.Gameplay.Magic.Binders
{
    public class MagicProjectilesUIBinder : GameService, IInitializable
    {
        private MagicAbilityComponent _magicAbilityComponent;
        private MagicProjectilesBarrel _magicProjectilesBarrel;

        private bool _isBinded;
        
        public override void Initialize()
        {
            _signalBus.Subscribe<PlayerInitializedSignal>(OnMagicComponentObtained);
            _signalBus.Subscribe<MagicProjectilesUIViewInitialized>(OnMagicProjectilesUIViewObtained);
            base.Initialize();
        }

        private void OnMagicProjectilesUIViewObtained(MagicProjectilesUIViewInitialized obj)
        {
            _magicProjectilesBarrel = obj.View;
            CheckComponents();
        }

        private void OnMagicComponentObtained(PlayerInitializedSignal obj)
        {
            _magicAbilityComponent = obj.Player.GetComponent<MagicAbilityComponent>();
            CheckComponents();
        }

        private void CheckComponents()
        {
            if (!_magicAbilityComponent || !_magicProjectilesBarrel)
                return;

            if (_isBinded)
                return;
            
            Bind(_magicAbilityComponent, _magicProjectilesBarrel);
        }


        private void Bind(MagicAbilityComponent magicAbilityComponent, MagicProjectilesBarrel magicProjectilesBarrel)
        {
            magicAbilityComponent.MagicPickupableProvided += magicProjectilesBarrel.OnMagicProjectileProvided;

            magicProjectilesBarrel.MagicTypeProvided += magicAbilityComponent.EmitMagicAbility;
            magicProjectilesBarrel.MagicTypeRemoved += magicAbilityComponent.RemoveProjectile;

            _isBinded = true;
        }
    }
}