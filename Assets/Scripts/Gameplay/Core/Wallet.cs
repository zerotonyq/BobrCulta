using Gameplay.Core.Base;
using Gameplay.Core.Pickup;
using Gameplay.Core.Pickup.Base;
using Gameplay.Services.Shop;

namespace Gameplay.Core
{
    public class Wallet : MonoComponent
    {
        public int Count { get; private set; }

        public void DecreaseCount(int amount)
        {
            if (Count - amount < 0)
            {
                Count = 0;
                return;
            }

            Count -= amount;
        }

        public override void Initialize()
        {
            GetComponent<PickupComponent>().PickedUp += TryBuy;
        }

        private void TryBuy(IPickupable obj)
        {
            if (obj is not Good good)
                return;
            
            good.Buy(this);
        }
    }
}