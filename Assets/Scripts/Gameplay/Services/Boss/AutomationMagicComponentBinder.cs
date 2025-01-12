using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Base;
using Gameplay.Magic;
using UnityEngine;

namespace Gameplay.Services.Boss
{
    [RequireComponent(typeof(MagicComponent))]
    public class AutomationMagicComponentBinder : Binder
    {
        private MagicComponent _magicComponent;

        [SerializeField] private List<AbilityInterval> _abilityIntervals = new();


        private Coroutine _automationCoroutine;

        public override void Bind()
        {
            _magicComponent = GetComponent<MagicComponent>();
            
            _automationCoroutine = StartCoroutine(AutomationCoroutine());
        }

        public void SetAbilityIntervals(List<AbilityInterval> abilityIntervals)
        {
            _abilityIntervals = abilityIntervals;
        }

        private IEnumerator AutomationCoroutine()
        {
            var index = 0;

            List<YieldInstruction> instructions = _abilityIntervals
                .Select(abilityInterval => new WaitForSeconds(abilityInterval.beforeInterval)).Cast<YieldInstruction>()
                .ToList();

            while (true)
            {
                _magicComponent.AddMagicAbilityPrefab(_abilityIntervals[index].pickupablePrefab.magicAbilityPrefab);
                _magicComponent.FireProjectile(_abilityIntervals[index].pickupablePrefab.magicAbilityPrefab.GetType());

                var current = instructions[index];

                index = index + 1 >= _abilityIntervals.Count ? 0 : index+1;

                yield return current;
            }
        }

        public void OnStopped() => StopCoroutine(_automationCoroutine);

        [Serializable]
        public struct AbilityInterval
        {
            public MagicPickupable pickupablePrefab;
            public float beforeInterval;
        }
    }
}