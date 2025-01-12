using Gameplay.Services.Base;
using Gameplay.Services.Difficulty.Config;
using Signals;
using Zenject;

namespace Gameplay.Services.Difficulty
{
    public class DifficultyService :GameService, IInitializable 

    {
        private int _currentDifficultySectionIndex;

        [Inject] private DifficultyConfig _config;
        [Inject] private SignalBus _signalBus;


    
        public override void Initialize()
        {
            _signalBus.Subscribe<NextDifficultySectionRequestSignal>(NextSection);
            
            base.Initialize();
        }

        public void NextSection()
        {
            if (_currentDifficultySectionIndex >= _config.sections.Count)
                _signalBus.Fire(new EndGameSignal() { Condition = true });

            _config.sections[_currentDifficultySectionIndex].GenerateBossConfigs();

            _signalBus.Fire(new NextDifficultySectionSignal()
            {
                DifficultySection = _config.sections[_currentDifficultySectionIndex]
            });

            ++_currentDifficultySectionIndex;
        }
    }
}