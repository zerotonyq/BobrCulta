using System.Collections.Generic;
using Gameplay.Magic;
using Gameplay.Magic.Pickupables.Base;

namespace Signals
{
    public class AllowedPickupablesSignal
    {
        public List<MagicPickupable> Allowed = new();
    }
}