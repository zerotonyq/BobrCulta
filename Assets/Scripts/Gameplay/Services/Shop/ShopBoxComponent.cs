using Gameplay.Coin;
using Gameplay.Core.Container;
using Gameplay.Services.Boxes;

namespace Gameplay.Services.Shop
{
    public class ShopBoxComponent : BoxComponent
    {
        private int _price;

        public void Initialize(Core.Pickup.Base.Pickupable prefab, int price)
        {
            _pickupablePrefabs.Clear();
            _pickupablePrefabs.Add(prefab);
            _price = price;
        }

        public override void Pickup(ComponentContainer pickuper)
        {
            if (!pickuper.TryGetComponent(out CoinComponent coinComponent))
                return;
            
            if (!coinComponent.TryDecreaseAmount(_price))
                return;

            base.Pickup(pickuper);
        }
    }
}