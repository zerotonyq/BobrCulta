using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Base;
using Gameplay.Magic;
using Gameplay.Magic.Abilities;
using Gameplay.Magic.Abilities.Base.Pickupable;
using Gameplay.Services.Boss.Config;
using Gameplay.Services.UI.Magic.Enum;
using Gameplay.Services.UI.Magic.Views;
using UnityEngine;

namespace Gameplay.Services.Boss
{
    [RequireComponent(typeof(MagicAbilityEmitter))]
    public class AutomationMagicComponentBinder : Binder
    {
        private MagicAbilityEmitter _magicAbilityEmitter;

        [SerializeField] private List<BossActivityConfig.BossConfig.AbilityInterval> _abilityIntervals = new();


        private Coroutine _automationCoroutine;

        public override void Bind()
        {
            _magicAbilityEmitter = GetComponent<MagicAbilityEmitter>();

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
                _magicAbilityEmitter.AddMagicAbilityPrefab(_abilityIntervals[index].abilityConfig.abilityPrefab);

                var type = _abilityIntervals[index].abilityConfig.abilityPrefab.GetType();
                
                _magicAbilityEmitter.EmitMagicAbility(new MagicProjectilesUIView.MagicTypeArgs(type, _abilityIntervals[index].abilityConfig.primaryApplicationType));

                var current = instructions[index];

                index = index + 1 >= _abilityIntervals.Count ? 0 : index + 1;
                
                yield return current;
            }
        }

        public void OnStopped() => StopCoroutine(_automationCoroutine);

        
    }
}