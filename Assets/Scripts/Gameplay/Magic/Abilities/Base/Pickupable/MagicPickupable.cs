using UnityEngine;

namespace Gameplay.Magic.Abilities.Base.Pickupable
{
    public abstract class MagicPickupable : Core.Pickup.Base.Pickupable
    {
        public MagicAbility magicAbilityPrefab;
        
        public Sprite projectileUISprite;

        public override void Reset() => transform.rotation = Quaternion.identity;
    }
}