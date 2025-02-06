using Gameplay.Core.Base;
using Gameplay.Core.Container;
using Gameplay.Core.Health;
using UnityEngine;
using Utils.Pooling;

namespace Gameplay.Magic.Abilities.WoodShield
{
    [RequireComponent(typeof(ComponentContainer), typeof(HealthComponent))]
    public class WoodShieldComponent : MonoComponent
    {
        public override void Initialize()
        {
            GetComponent<HealthComponent>().Dead += OnDead;
        }

        private void OnDead()
        {
            GetComponent<ComponentContainer>().Deactivate();
            
            PoolManager.AddToPool(GetType(), gameObject);
        }
    }
}