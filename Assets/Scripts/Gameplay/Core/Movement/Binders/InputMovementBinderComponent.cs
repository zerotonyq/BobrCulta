using Gameplay.Core.Base;
using Input;
using R3;
using UnityEngine;
using Utils.ObservableExtension;

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
                    .ReadValue<Vector2>()));

            var subscribtion2 = InputProvider.InputSystemActions.Player.Move.ToObservableCanceled().Subscribe(ctx => movementComponent.AddAcceleration(ctx.ReadValue<Vector2>()));
                
            DisposableBag.Add(subscription);
            DisposableBag.Add(subscribtion2);
        }
    }
}