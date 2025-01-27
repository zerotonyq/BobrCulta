using Gameplay.Core.Container;

namespace Signals.Player
{
    public struct PlayerInitializedSignal
    {
        public ComponentContainer Player { get; set; }
    }
}