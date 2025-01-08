using Gameplay.Core.Health;
using Gameplay.Magic.Abilities.Base;
using UnityEngine;

namespace Gameplay.Magic.Abilities.Shield
{
    public class ShieldMagicAbility : MagicAbility, IHoldableAbility
    {
        private HealthComponent _currentHealthComponent;

       
        public override void Emit(Transform emitter, Transform target)
        {
            base.Emit(emitter, target);

            if (!emitter.TryGetComponent(out HealthComponent healthComponent))
            {
                Debug.LogError("There is no health component on entity");
                return;
            }
            
            transform.SetParent(emitter);
            transform.localPosition = Vector3.zero;
            
            _currentHealthComponent = healthComponent;

            healthComponent.AllowDecreaseHealth(false);
        }

        public void Unhold()
        {
            if (_currentHealthComponent)
                _currentHealthComponent.AllowDecreaseHealth(true);

            if (!_isActivated)
                return;
            
            Deactivate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            Destroy(gameObject);
        }
    }
}