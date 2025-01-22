using Gameplay.Core.Base;
using Input;
using R3;
using UnityEngine;
using Utils.ObservableExtension;

namespace Gameplay.Core.Pickup
{
    public class PickupBinderComponent : Binder
    {
        
        public override void Bind()
        {
            var pickupComponent = GetComponent<PickupComponent>();

            var disposable = InputProvider.InputSystemActions.Player.InsertMagicProjectile.ToObservablePerformed().Subscribe(ctx =>
            {
                pickupComponent.Pickup();
            });
            DisposableBag.Add(disposable);
        }
    }
}