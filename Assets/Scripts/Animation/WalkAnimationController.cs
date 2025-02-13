using Gameplay.Core.Base;
using Input;
using R3;
using UnityEngine;
using Utils.ObservableExtension;

namespace Animation
{
    [RequireComponent(typeof(Animator))]
    public class WalkAnimationController : MonoComponent
    {
        private int _hashX;
        private int _hashY;

        private Animator _animator;

        public override void Initialize()
        {
            _hashX = Animator.StringToHash("x");
            _hashY = Animator.StringToHash("y");

            _animator = GetComponent<Animator>();

            InputProvider.InputSystemActions.Player.Move.ToObservablePerformed().Subscribe((ctx) =>
            {
                var v = ctx.ReadValue<Vector2>();
                _animator.SetFloat(_hashX, v.x);
                _animator.SetFloat(_hashY, v.y);
            }).AddTo(this);
            
            InputProvider.InputSystemActions.Player.Move.ToObservableCanceled().Subscribe((ctx) =>
            {
                var v = ctx.ReadValue<Vector2>();
                _animator.SetFloat(_hashX, v.x);
                _animator.SetFloat(_hashY, v.y);
            }).AddTo(this);
        }
    }
}