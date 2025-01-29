using System;
using Gameplay.Coin.Pickupable;
using Gameplay.Core.Base;
using Gameplay.Core.Health;
using Gameplay.Core.Pickup;
using Gameplay.Core.Pickup.Base;
using Gameplay.Services.Shop;
using UnityEngine;
using Utils.Pooling;
using Random = UnityEngine.Random;

namespace Gameplay.Coin
{
    public class CoinComponent : MonoComponent
    {
        [SerializeField] private int initialAmount;
        public int Amount => _amount;

        private PickupComponent _pickupComponent;
        private HealthComponent _healthComponent;

        private int _amount;

        public override void Initialize()
        {
            _pickupComponent = GetComponent<PickupComponent>();
            _healthComponent = GetComponent<HealthComponent>();

            _healthComponent.HealthDecreased += TryEmit;
            _pickupComponent.PickedUp += OnPickupObtained;

            _amount = initialAmount;
        }

        public bool TryDecreaseAmount(int price)
        {
            if (price > _amount)
            {
                Debug.Log("CANNOT MAKE AMOUNT NEGATIVE");
                return false;
            }

            _amount -= price;
            return true;
        }

        private void TryEmit()
        {
            if (_amount <= 0)
                return;

            var coinPickupable = PoolManager.GetFromPool(typeof(CoinPickupable)).GetComponent<CoinPickupable>();

            coinPickupable.Activate(transform.position + Vector3.up * GetComponent<Collider>().bounds.size.y / 2 +
                                    Vector3.up * 2);

            coinPickupable.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-3, 3), 10, Random.Range(-3, 3)),
                ForceMode.Impulse);

            --_amount;
        }

        private void OnPickupObtained(IPickupable obj)
        {
            if (obj is not CoinPickupable) 
                return;

            ++_amount;
        }

        public override void Reset() => _amount = 0;
    }
}