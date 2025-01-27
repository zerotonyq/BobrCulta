using System;
using Gameplay.Core;
using Gameplay.Core.Pickup.Base;
using UnityEngine;
using Utils.Activate;
using Utils.Pooling;

namespace Gameplay.Services.Shop
{
    public class Good : MonoBehaviour, IPickupable, IActivateable
    {
        private Pickupable _pickupablePrefab;
        private int _price;


        public void Initialize(Pickupable prefab, int price)
        {
            _pickupablePrefab = prefab;
            _price = price;
        }
        
        public void Buy(Wallet wallet)
        {
            if (wallet.Count < _price)
                return;

            var pickupable = PoolManager.GetFromPool(_pickupablePrefab.GetType(), _pickupablePrefab.gameObject);
            pickupable.GetComponent<Rigidbody>().AddForce(Vector3.up * 5);
            Deactivate();
        }

        public Action<GameObject> Deactivated { get; set; }
        public void Activate(Vector3 position)
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Pickup()
        {
            
        }
    }
}