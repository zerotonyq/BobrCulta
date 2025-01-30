using Gameplay.Core.Base;
using Gameplay.Magic.Abilities;

namespace Gameplay.Magic.Barrel
{
    public class BarrelMagicComponentBinder : MonoComponent
    {
        private MagicPickupableBarrelComponent _magicPickupableBarrelComponent;
        private MagicAbilityComponent _magicAbilityComponent;

        public override void Initialize()
        {
            _magicPickupableBarrelComponent = GetComponent<MagicPickupableBarrelComponent>();
            _magicAbilityComponent = GetComponent<MagicAbilityComponent>();

            _magicPickupableBarrelComponent.MagicPickupableProvided += _magicAbilityComponent.EmitMagicAbility;
        }
    }
}