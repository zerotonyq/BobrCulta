using Gameplay.Magic;
using Gameplay.Magic.Abilities;
using Gameplay.Services.Base;
using Gameplay.Services.UI.Magic.Views;
using Signals;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.UI.Magic.Binders
{
    public class MagicProjectilesUIBinder : GameService, IInitializable
    {
        [Inject] private SignalBus _signalBus;

        private AbilityEmitter _abilityEmitter;
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
            _abilityEmitter = obj.Player.GetComponent<AbilityEmitter>();
            CheckComponents();
        }

        private void CheckComponents()
        {
            if (!_abilityEmitter || !_magicProjectilesUIView)
                return;

            Bind(_abilityEmitter, _magicProjectilesUIView);
        }


        private void Bind(AbilityEmitter abilityEmitter, MagicProjectilesUIView magicProjectilesUIView)
        {
            abilityEmitter.MagicPickupableProvided += magicProjectilesUIView.OnMagicProjectileProvided;

            magicProjectilesUIView.MagicTypeProvided += abilityEmitter.EmitMagicAbility;
            magicProjectilesUIView.MagicTypeRemoved += abilityEmitter.RemoveProjectile;
        }
    }
}