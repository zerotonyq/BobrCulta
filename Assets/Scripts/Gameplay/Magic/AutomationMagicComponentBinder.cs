using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Base;
using Gameplay.Magic.Abilities.Base;
using UnityEngine;

namespace Gameplay.Magic
{
    [RequireComponent(typeof(MagicComponent))]
    public class AutomationMagicComponentBinder : Binder
    {
        private MagicComponent _magicComponent;

        [SerializeField] private List<AbilityInterval> abilityIntervals = new();


        private Coroutine _automationCoroutine;

        public override void Bind()
        {
            _magicComponent = GetComponent<MagicComponent>();
            
            _automationCoroutine = StartCoroutine(AutomationCoroutine());
        }

        private IEnumerator AutomationCoroutine()
        {
            var index = 0;

            List<YieldInstruction> instructions = abilityIntervals
                .Select(abilityInterval => new WaitForSeconds(abilityInterval.beforeInterval)).Cast<YieldInstruction>()
                .ToList();

            while (true)
            {
                _magicComponent.AddMagicAbilityPrefab(abilityIntervals[index].abilityPrefab);
                _magicComponent.FireProjectile(abilityIntervals[index].abilityPrefab.GetType());

                var current = instructions[index];

                ++index;

                index = index >= abilityIntervals.Count ? 0 : index;

                yield return current;
            }
        }

        public void OnStopped() => StopCoroutine(_automationCoroutine);

        [Serializable]
        public struct AbilityInterval
        {
            public MagicAbility abilityPrefab;
            public float beforeInterval;
        }
    }
}