using Gameplay.Core.Pickup.Base;
using Gameplay.Magic.Projectiles.Base;
using UnityEngine;

namespace Gameplay.Magic.Pickupables.Base
{
    public abstract class MagicPickupable : MonoBehaviour, IPickupable
    {
        public MagicProjectile MagicProjectilePrefab;
        
        public Sprite projectileUISprite;
    }
}