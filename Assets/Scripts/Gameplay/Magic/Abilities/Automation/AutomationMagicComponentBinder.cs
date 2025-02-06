using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Base;
using Gameplay.Magic.Abilities;
using Gameplay.Magic.Barrel;
using Gameplay.Services.Boss.Config;
using UnityEngine;

namespace Gameplay.Services.Boss
{
    [RequireComponent(typeof(MagicAbilityComponent))]
    public class AutomationMagicComponentBinder : Binder
    {
        private MagicAbilityComponent _magicAbilityComponent;

        [SerializeField] private List<BossActivityConfig.BossConfig.AbilityInterval> _abilityIntervals = new();


        private Coroutine _automationCoroutine;

        public override void Bind()
        {
            _magicAbilityComponent = GetComponent<MagicAbilityComponent>();

            _automationCoroutine = StartCoroutine(AutomationCoroutine());
        }

        public void SetAbilityIntervals(List<BossActivityConfig.BossConfig.AbilityInterval> abilityIntervals)
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
                var current = instructions[index];
                
                yield return current;
                
                _magicAbilityComponent.EmitMagicAbility(new MagicPickupableBarrelComponent.MagicTypeArgs(
                    _abilityIntervals[index].pickupable,
                    _abilityIntervals[index].pickupable.magicAbilityPrefab.PrimaryApplicationType));


                index = index + 1 >= _abilityIntervals.Count ? 0 : index + 1;
            }
        }

        public void OnStopped() => StopCoroutine(_automationCoroutine);
    }
}