using Gameplay.Services.Base;
using Gameplay.Services.Difficulty.Config;
using Signals;
using Zenject;

namespace Gameplay.Services.Difficulty
{
    public class DifficultyService : GameService, IInitializable
    {
        [Inject] private DifficultyConfig _config;

        private int _currentIndex;

        public override void Initialize()
        {
            _signalBus.Subscribe<NextChapterRequest>(NextSection);
            base.Initialize();
        }
        
        private void NextSection()
        {
            if (_currentIndex == _config.sections.Count - 1)
                _signalBus.Fire(new EndGameSignal() { Condition = true });

            _signalBus.Fire(new NextDifficultySectionProvided { Section = _config.sections[_currentIndex] });

            ++_currentIndex;
        }
    }
}