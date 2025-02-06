using Gameplay.Core.Container;
using Gameplay.Core.Ground;
using Gameplay.Core.TargetTracking;
using Gameplay.Magic.Abilities.Base;
using Gameplay.Magic.Abilities.WoodShield;
using UnityEngine;
using Utils.Pooling;

namespace Gameplay.Magic.Abilities.RepeatCannon
{
    public class RepeatCannonMagicAbility : MagicAbility
    {
        [SerializeField] private RepeatCannonComponent repeatCannonPrefab;
        
        public override void OnTriggerEnter(Collider other)
        {
            if (CheckSelfCollision(other))
                return;

            if (!other.TryGetComponent(out GroundComponent _))
            {
                Deactivate();
                return;
            }

            var repeatCannon = PoolManager.GetFromPool(repeatCannonPrefab.GetType(), repeatCannonPrefab.gameObject).GetComponent<RepeatCannonComponent>();

            repeatCannon.GetComponent<TargetTrackingComponent>().SetTarget(GetComponent<TargetTrackingComponent>().Target);
            
            repeatCannon.GetComponent<ComponentContainer>().Activate(transform.position);
            
            repeatCannon.GetComponent<ComponentContainer>().Initialize();
            
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            Reset();
            
            Deactivate();
        }
    }
}