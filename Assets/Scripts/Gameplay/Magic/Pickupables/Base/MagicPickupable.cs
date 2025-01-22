using Gameplay.Core.Pickup.Base;
using Gameplay.Magic.Abilities.Base;
using UnityEngine;

namespace Gameplay.Magic
{
    public class MagicPickupable : MonoBehaviour, IPickupable
    {
        public MagicAbility magicAbilityPrefab;
        
        public Sprite projectileUISprite;
    }
}