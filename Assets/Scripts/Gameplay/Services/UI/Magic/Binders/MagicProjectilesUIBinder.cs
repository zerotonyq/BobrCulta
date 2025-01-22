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
        private MagicAbilityEmitter _magicAbilityEmitter;
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
            _magicAbilityEmitter = obj.Player.GetComponent<MagicAbilityEmitter>();
            CheckComponents();
        }

        private void CheckComponents()
        {
            if (!_magicAbilityEmitter || !_magicProjectilesUIView)
                return;

            Bind(_magicAbilityEmitter, _magicProjectilesUIView);
        }


        private void Bind(MagicAbilityEmitter magicAbilityEmitter, MagicProjectilesUIView magicProjectilesUIView)
        {
            magicAbilityEmitter.MagicPickupableProvided += magicProjectilesUIView.OnMagicProjectileProvided;

            magicProjectilesUIView.MagicTypeProvided += magicAbilityEmitter.EmitMagicAbility;
            magicProjectilesUIView.MagicTypeRemoved += magicAbilityEmitter.RemoveProjectile;
        }
    }
}