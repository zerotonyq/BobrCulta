using Gameplay.Core.Base;
using R3;

namespace Gameplay.Core.Rotation
{
    public class RotationBinderComponent : Binder
    {
        public override void Bind()
        {
            var targetLookRotationComponent = GetComponent<TargetLookRotationComponent>();
            
            var disposable = Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ =>
                targetLookRotationComponent.Rotate());

            DisposableBag.Add(disposable);
        }
    }
}