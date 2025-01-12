using System;
using UnityEngine;

namespace Gameplay.Core.TargetTracking.Provider
{
    //TODO: переписать под event bus
    public static class TargetProvider
    {
        private static Transform _bossTransform;
        private static Transform _playerTransform;

        public static Action<Transform> BossTargetProvided;
        public static Action<Transform> PlayerTargetProvided;
        
        public static void SetBoss(Transform newTarget)
        {
            _bossTransform = newTarget;
            BossTargetProvided?.Invoke(_bossTransform);
        }

        public static void SetPlayer(Transform newTarget)
        {
            _playerTransform = newTarget;
            PlayerTargetProvided?.Invoke(_playerTransform);
        }

        public static Transform GetPlayer() => _playerTransform;

        public static Transform GetBoss() => _bossTransform;
    }
}