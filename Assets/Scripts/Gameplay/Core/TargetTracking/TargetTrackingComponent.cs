using System;
using Gameplay.Core.Base;
using Gameplay.Core.TargetTracking.Provider;
using UnityEngine;
using Utils.Reset;

namespace Gameplay.Core.TargetTracking
{
    public class TargetTrackingComponent : MonoComponent
    {
        [SerializeField] private TargetType targetType;
       [field: SerializeField] public Transform Target { get; private set; }

        public Action<Transform> TargetChanged;

        public override void Initialize()
        {
            switch (targetType)
            {
                case TargetType.None:
                    return;
                case TargetType.Enemy:
                    TargetProvider.BossTargetProvided += SetTarget;
                    SetTarget(TargetProvider.GetBoss());
                    break;
                case TargetType.Player:
                    TargetProvider.PlayerTargetProvided += SetTarget;
                    SetTarget(TargetProvider.GetPlayer());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetTarget(Transform target)
        {
            Target = target;
            TargetChanged?.Invoke(target);
        }

        private enum TargetType
        {
            None,
            Enemy,
            Player
        }

        public override void Reset() => SetTarget(null);
    }
}