using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Core.Container;
using Gameplay.Core.Pickup.Base;
using UnityEngine;
using UnityEngine.Serialization;
using Utils.Activate;
using Utils.Pooling;
using Utils.Reset;
using Random = UnityEngine.Random;

namespace Gameplay.Services.Boxes
{
    public class BoxComponent : Pickupable
    {
        [SerializeField] private float emissionDelay;
        [SerializeField] private float emissionHorizontalForce;
        [SerializeField] private float emissionVerticalForce;

        private YieldInstruction _delayInstruction;

        private Coroutine _currentEmissionCoroutine;

        protected List<Pickupable> _pickupablePrefabs = new();

        public void Initialize(List<Pickupable> pickupablePrefabs)
        {
            _pickupablePrefabs = pickupablePrefabs;
            _delayInstruction = new WaitForSeconds(emissionDelay);
        }

        public override void Pickup(ComponentContainer pickuper)
        {
            if (_currentEmissionCoroutine != null)
                return;

            _currentEmissionCoroutine = StartCoroutine(EmissionCoroutine());
        }

        private IEnumerator EmissionCoroutine()
        {
            yield return _delayInstruction;

            foreach (var pickupable in _pickupablePrefabs)
            {
                var instantiatedPickupable = PoolManager.GetFromPool(pickupable.GetType(), pickupable.gameObject)
                    .GetComponent<Pickupable>();

                instantiatedPickupable.Activate(
                    transform.position + Vector3.up * GetComponent<Collider>().bounds.size.y);

                var body = instantiatedPickupable.GetComponent<Rigidbody>();

                var emissionDirection = Vector3.up * emissionVerticalForce + new Vector3(
                    Random.Range(-emissionHorizontalForce, emissionHorizontalForce), 0,
                    Random.Range(-emissionHorizontalForce, emissionHorizontalForce));

                body.AddForce(emissionDirection, ForceMode.Impulse);

                yield return _delayInstruction;
            }

            Deactivate();
        }

        

        public override void Activate(Vector3 position)
        {
            transform.position = position;
            Deactivated?.Invoke(gameObject);
            gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            if (_currentEmissionCoroutine != null)
            {
                StopCoroutine(_currentEmissionCoroutine);
                _currentEmissionCoroutine = null;
            }
            
            Reset();
            PoolManager.AddToPool(GetType(), gameObject);
            gameObject.SetActive(false);
        }

        public override void Reset()
        {
            base.Reset();
            transform.rotation = Quaternion.identity;
        }
    }
}