using System.Collections.Generic;
using Gameplay.Magic;
using Gameplay.Magic.Abilities.Base.Pickupable;

namespace Signals
{
    public class AllowedPickupablesSignal
    {
        public List<MagicPickupable> Allowed = new();
    }
}