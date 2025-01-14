using Gameplay.Services.Base;
using Gameplay.Services.Boss;
using Gameplay.Services.Difficulty;
using Signals;
using Zenject;

namespace Gameplay.Services.Level
{
    public class LevelService : GameService, IInitializable
    {
        
        [Inject] private BossService _bossService;
        [Inject] private DifficultyService _difficultyService;
        [Inject] private SignalBus _signalBus;
        
        public override async void Initialize()
        {
            _signalBus.Subscribe<NextLevelRequest>(OnNextLevelRequested);
            base.Initialize();
        }

        private void OnNextLevelRequested()
        {
            _difficultyService.NextSection();

            _bossService.SpawnNext();
        }
    }
}