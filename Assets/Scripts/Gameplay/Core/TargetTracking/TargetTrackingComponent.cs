using System;
using Gameplay.Core.Base;
using Gameplay.Core.TargetTracking.Provider;
using UnityEngine;

namespace Gameplay.Core.TargetTracking
{
    public class TargetTrackingComponent : MonoComponent
    {
        public Transform Target { get; private set; }

        public Action<Transform> TargetChanged;
        
        public override void Initialize() => TargetProvider.TargetProvided += SetTarget;

        private void SetTarget(Transform target)
        {
            Target = target;
            TargetChanged?.Invoke(target);
        }
    }
}