using System;
using UnityEngine;

namespace Gameplay.Core.TargetTracking.Provider
{
    public static class TargetProvider
    {
        public static Transform Target { get; private set; }

        public static Action<Transform> TargetProvided;
        
        public static void SetTarget(Transform newTarget)
        {
            Target = newTarget;
            TargetProvided?.Invoke(Target);
        }
    }
}