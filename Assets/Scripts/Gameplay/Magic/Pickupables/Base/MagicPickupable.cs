using System;
using Gameplay.Core.Pickup.Base;
using Gameplay.Magic.Abilities.Base;
using UnityEngine;
using Utils.Activate;
using Utils.Initialize;
using Utils.Pooling;

namespace Gameplay.Magic.Pickupables.Base
{
    public class MagicPickupable : MonoBehaviour, IPickupable, IActivateable, IInitializableMono
    {
        public MagicAbility magicAbilityPrefab;
        
        public Sprite projectileUISprite;
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);

            PoolManager.AddToPool(GetType(), gameObject);
        }

        public void Pickup()
        {
            Deactivate();
        }

        public void Initialize()
        {
            
        }
    }
}