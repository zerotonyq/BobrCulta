using Gameplay.Core.Base;
using Input;
using R3;
using UnityEngine;

namespace Gameplay.Core.Movement.Binders
{
    [RequireComponent(typeof(MovementComponent))]
    public class InputMovementBinderComponent : Binder
    {
        public override void Bind()
        {
            var movementComponent = GetComponent<MovementComponent>();

            var subscription = Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ =>
                movementComponent.AddAcceleration(InputProvider.InputSystemActions.Player.Move
                    .ReadValue<Vector2>())).AddTo(this);

            DisposableBag.Add(subscription);
        }
    }
}