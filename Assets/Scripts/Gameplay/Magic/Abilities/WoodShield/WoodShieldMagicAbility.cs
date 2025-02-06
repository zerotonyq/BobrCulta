using Gameplay.Core.Base;
using Gameplay.Core.Container;
using Gameplay.Core.Ground;
using Gameplay.Magic.Abilities.Base;
using UnityEngine;
using Utils.Pooling;

namespace Gameplay.Magic.Abilities.WoodShield
{
    public class WoodShieldMagicAbility : MagicAbility
    {
        [SerializeField] private WoodShieldComponent woodShieldPrefab;
        
        public override void OnTriggerEnter(Collider other)
        {
            if (CheckSelfCollision(other))
                return;

            if (!other.TryGetComponent(out GroundComponent _))
            {
                Deactivate();
                return;
            }

            var woodShield = PoolManager.GetFromPool(woodShieldPrefab.GetType(), woodShieldPrefab.gameObject).GetComponent<WoodShieldComponent>();

            woodShield.GetComponent<ComponentContainer>().Activate(transform.position);
            
            woodShield.GetComponent<ComponentContainer>().Initialize();
            
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            Reset();
            
            Deactivate();
        }
    }
}