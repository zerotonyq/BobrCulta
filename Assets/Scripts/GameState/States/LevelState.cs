using System.Linq;
using Signals;
using Signals.GameStates;
using Signals.Level;
using Utils.Reset;

namespace GameState.States
{
    public class LevelState : Base.GameState
    {
        public LevelState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
            gameStateMachine.SignalBus.Subscribe<LevelPassedSignal>(OnLevelPassed);
        }

        private void OnLevelPassed(LevelPassedSignal signal)
        {
            if (signal.IsWin)
            {
                _gameStateMachine.SignalBus.Fire<NextLevelRequest>();
                return;
            }
            
            _gameStateMachine.SignalBus.Fire(new EndGameRequest{IsWin = false});
        }

        public override void Enter()
        {
            base.Enter();
            
            ResetServices();
            
            _gameStateMachine.SignalBus.Fire(new LevelPassedSignal{IsWin = true});
        }

        private void ResetServices()
        {
            var services = _gameStateMachine.Services.OfType<IResetable>().ToList();

            foreach (var service in services) 
                service.Reset();
        }
    }
}