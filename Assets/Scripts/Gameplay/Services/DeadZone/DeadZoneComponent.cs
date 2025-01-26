using System;
using Gameplay.Core.Base;
using UnityEngine;

namespace Gameplay.Services.DeadZone
{
    [RequireComponent(typeof(BoxCollider))]
    public class DeadZoneComponent : MonoBehaviour
    {
        public Action<Collider> ColliderDetected;
        
        public void Initialize(float dimension)
        {
            GetComponent<BoxCollider>().size = new Vector3(dimension, 5, dimension);
        }

        private void OnTriggerEnter(Collider other)
        {
            ColliderDetected?.Invoke(other);
        }
    }
}