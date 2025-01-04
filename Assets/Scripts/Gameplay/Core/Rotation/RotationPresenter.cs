using System;
using System.Threading.Tasks;
using Gameplay.Boss;
using Gameplay.Core.Rotation.Config;
using R3;
using UnityEngine;
using Utils.Initialize;

namespace Gameplay.Core.Rotation
{
    public class RotationPresenter : MonoBehaviour, IInitializableConfig
    {
        private RotationComponent _rotationComponent;
        private DisposableBag _disposableBag;
        
        public Task Initialize(ScriptableObject config)
        {
            _rotationComponent = new RotationComponent();

            _rotationComponent.Initialize((RotationConfig)config);

            _rotationComponent.SetTransform(transform);

             var a = Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ =>
                _rotationComponent.RotateTowardsTarget()).AddTo(this);
             
             _disposableBag.Add(a);

            return Task.CompletedTask;
        }

        private void OnDestroy() => _disposableBag.Dispose();

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}