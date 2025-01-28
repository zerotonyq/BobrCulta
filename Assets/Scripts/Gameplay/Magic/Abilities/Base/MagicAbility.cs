using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Container;
using Gameplay.Core.Movement.Binders;
using Gameplay.Magic.Abilities.Base.Config;
using Gameplay.Magic.Effects;
using Gameplay.Magic.Effects.Base;
using Gameplay.Services.UI.Gameplay.Magic.Enum;
using UnityEngine;
using Utils.Activate;
using Utils.Initialize;
using Utils.Pooling;
using Utils.Reset;

namespace Gameplay.Magic.Abilities.Base
{
    [RequireComponent(typeof(TargetMovementBinderComponent))]
    public abstract class MagicAbility : MonoBehaviour, IInitializableMono, IActivateable
    {
        public bool Initialized { get; private set; }
        
        [SerializeField] private MagicAbilityConfig config;

        private List<Type> _antagonistTypes = new();

        private Dictionary<Effect, EffectConfig> _effects = new();

        private int _effectCompletionCount;

        private Collider _emitterCollider;

        public Action<GameObject> Deactivated { get; set; }

        private TargetMovementBinderComponent _targetMovementBinder;
        
        public void Initialize()
        {
            if (Initialized)
                return;
            
            _antagonistTypes = config.antagonistAbilities.Select(a => a.GetAbilityType()).ToList();

            if (_effects.Count == 0)
            {
                foreach (var effectConfig in config.effectConfigs)
                    _effects.Add(effectConfig.GetEffect(), effectConfig);
            }

            _targetMovementBinder = GetComponent<TargetMovementBinderComponent>();
            _targetMovementBinder.Bind();

            Initialized = true;
        }

        public void Use(Transform emitter, Transform target, ApplicationType applicationType)
        {
            switch (applicationType)
            {
                case ApplicationType.Fire:
                {
                    GetComponent<Collider>().enabled = true;
                    GetComponent<Rigidbody>().isKinematic = false;
                    _emitterCollider = emitter.GetComponent<Collider>();
                    
                    GetComponent<TargetMovementBinderComponent>().SetTarget(target);

                    break;
                }
                case ApplicationType.ApplyToItself:
                {
                    //TODO hide gfx too
                    GetComponent<Collider>().enabled = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    
                    emitter.GetComponent<AbilityHandler>().AttachAbility(this, _antagonistTypes);

                    break;
                }
            }
        }

        public void Activate(Vector3 position)
        {
            transform.position = position;

            gameObject.SetActive(true);

            foreach (var component in GetComponents<MonoBehaviour>())
            {
                component.enabled = true;
            }
        }

        public void Deactivate()
        {
            Deactivated?.Invoke(gameObject);

            Reset();
            
            gameObject.SetActive(false);

            PoolManager.AddToPool(GetType(), gameObject);
        }

        public void Reset()
        {
            ResetEffects();
            
            foreach (var resetable in GetComponents<IResetable>())
            {
                resetable.Reset();
            }
        }
        
        #region Effects

        public void ApplyEffects(ComponentContainer componentContainer)
        {
            if (!gameObject.activeSelf)
                return;
            
            ResetEffects();

            foreach (var effectAndConfig in _effects)
            {
                effectAndConfig.Key.SetupDuration(effectAndConfig.Value.durationType, effectAndConfig.Value.duration);

                effectAndConfig.Key.EffectElapsed += UpdateEffectCompletionCount;

                effectAndConfig.Key.CurrentCoroutine = StartCoroutine(effectAndConfig.Key.Apply(componentContainer));
            }
        }

        private void ResetEffects()
        {
            _effectCompletionCount = 0;

            foreach (var effect in _effects.Keys)
            {
                if (effect.CurrentCoroutine != null)
                    StopCoroutine(effect.CurrentCoroutine);

                effect.EffectElapsed -= UpdateEffectCompletionCount;

                effect.Reset();
            }
        }

        private void UpdateEffectCompletionCount(Effect effect)
        {
            ++_effectCompletionCount;

            if (_effectCompletionCount >= _effects.Keys.Count) Deactivate();
        }

        #endregion

        #region CollisionDetection

        private bool CheckSelfCollision(Collider other)
        {
            if (_emitterCollider == null)
                return false;
            
            if (other == _emitterCollider)
            {
                Debug.Log("Same emitter");
                return true;
            }

            if (other.transform.IsChildOf(_emitterCollider.transform))
                return true;

            return false;
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            
            if (CheckSelfCollision(other))
                return;

            if (!other.TryGetComponent(out AbilityHandler container))
            {
                Deactivate();
                return;
            }
            

            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            
            Reset();
            
            container.AttachAbility(this, _antagonistTypes);
        }

        #endregion
        
    }
}