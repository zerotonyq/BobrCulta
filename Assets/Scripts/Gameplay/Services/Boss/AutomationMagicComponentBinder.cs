using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Base;
using Gameplay.Magic;
using Gameplay.Magic.Abilities;
using Gameplay.Magic.Pickupables.Base;
using Gameplay.Services.UI.Magic.Enum;
using Gameplay.Services.UI.Magic.Views;
using UnityEngine;

namespace Gameplay.Services.Boss
{
    [RequireComponent(typeof(AbilityEmitter))]
    public class AutomationMagicComponentBinder : Binder
    {
        private AbilityEmitter _abilityEmitter;

        [SerializeField] private List<AbilityInterval> _abilityIntervals = new();


        private Coroutine _automationCoroutine;

        public override void Bind()
        {
            _abilityEmitter = GetComponent<AbilityEmitter>();

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
                _abilityEmitter.AddMagicAbilityPrefab(_abilityIntervals[index].pickupablePrefab.magicAbilityPrefab);

                var type = _abilityIntervals[index].pickupablePrefab.magicAbilityPrefab.GetType();
                
                _abilityEmitter.EmitMagicAbility(new MagicProjectilesUIView.MagicTypeArgs(type, _abilityIntervals[index].applicationType));

                var current = instructions[index];

                index = index + 1 >= _abilityIntervals.Count ? 0 : index + 1;
                
                yield return current;
            }
        }

        public void OnStopped() => StopCoroutine(_automationCoroutine);

        [Serializable]
        public struct AbilityInterval
        {
            public MagicPickupable pickupablePrefab;
            public ApplicationType applicationType;
            public float beforeInterval;
        }
    }
}