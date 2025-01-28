using UnityEngine;

namespace Gameplay.Coin.Pickupable
{
    public class CoinPickupable : Core.Pickup.Base.Pickupable
    {
        public override void Reset() => transform.rotation = Quaternion.identity;
    }
}