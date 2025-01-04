using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Core.Movement.Physical;
using Gameplay.Core.Movement.Physical.Config;
using Input;
using R3;
using Unity.VisualScripting;
using UnityEngine;
using Utils.Initialize;

namespace Gameplay.Core.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementPresenter : MonoBehaviour, IInitializableConfig
    {
        private PhysicalMovementComponent _physicalMovementComponent;
        private DisposableBag _disposableBag;

        public Task Initialize(ScriptableObject config)
        {
            _physicalMovementComponent = new PhysicalMovementComponent();

            _physicalMovementComponent.Initialize((PhysicsMovementConfig)config);

            _physicalMovementComponent.SetRigidbody(GetComponent<Rigidbody>());

            var a =Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ =>
                _physicalMovementComponent.AddAcceleration(InputProvider.InputSystemActions.Player.Move
                    .ReadValue<Vector2>())).AddTo(this);
            _disposableBag.Add(a);

            return Task.CompletedTask;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, GetComponent<Rigidbody>().linearVelocity);
        }

        private void OnDestroy() => _disposableBag.Dispose();

        public void Dispose()
        {
        }
    }
}