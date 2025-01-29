using Gameplay.Core.Container;

namespace Gameplay.Core.Pickup.Base
{
    public interface IPickupable
    {
        void Pickup(ComponentContainer pickuper);
    }
}