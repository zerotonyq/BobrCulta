using Gameplay.Core.Container;

namespace Gameplay.Magic.Abilities.Base.Pickupable
{
    public class MagicPickupable : Core.Pickup.Base.Pickupable
    {
        public MagicAbility magicAbilityPrefab;
        
        public override void Pickup(ComponentContainer pickuper)
        {
            PickedUp?.Invoke(this);
        }
    }
}