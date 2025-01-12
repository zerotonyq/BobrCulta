using System.Threading.Tasks;
using Signals;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.Base
{
    public abstract class GameService : IGameService
    {
        [Inject] protected SignalBus _signalBus;

        public virtual async void Boot()
        {
            _signalBus.Fire<ServiceBootEndRequest>();

        }

        public virtual void Initialize()
        {
            _signalBus.Subscribe<BootRequest>(Boot);
            _signalBus.Fire<InitializedServiceSignal>();
        }
    }
}