using System.Collections;
using System.Collections.Generic;
using Gameplay.Core.Pickup.Base;
using UnityEngine;
using Utils.Activate;
using Utils.Pooling;
using Utils.Reset;
using Random = UnityEngine.Random;

namespace Gameplay.Magic.Boxes
{
    public class BoxComponent : MonoBehaviour, IActivateable, IResetable
    {
        [SerializeField] private float emissionDelay;
        [SerializeField] private float radius;
        
        private YieldInstruction _delayInstruction;
        
        private Coroutine _currentEmissionCoroutine;
        
        private List<Pickupable> _pickupables = new();
        
        public void Initialize(List<Pickupable> pickupables)
        {
            _pickupables = pickupables;
            _delayInstruction = new WaitForSeconds(emissionDelay);
        }

        public void Use()
        {
            if (_currentEmissionCoroutine != null)
                return;

            _currentEmissionCoroutine = StartCoroutine(EmissionCoroutine());
        }

        private IEnumerator EmissionCoroutine()
        {
            yield return _delayInstruction;
            
            foreach (var pickupable in _pickupables)
            {
                pickupable.Activate(transform.position);
                
                var body = pickupable.GetComponent<Rigidbody>();

                var emissionDirection = transform.position + Vector3.up +
                                        new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
                
                body.AddForce(emissionDirection, ForceMode.Impulse);
                
                yield return _delayInstruction;
            }
        }

        public void Activate(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            Reset();
            PoolManager.AddToPool(GetType(), gameObject);
        }

        public void Reset() => transform.rotation = Quaternion.identity;
    }
}