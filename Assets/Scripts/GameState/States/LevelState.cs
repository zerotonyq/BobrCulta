using System;
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
            switch (signal.PassedType)
            {
                case LevelPassedSignal.LevelPassedType.None:
                    break;
                case LevelPassedSignal.LevelPassedType.Loose:
                    _gameStateMachine.SignalBus.Fire(new EndGameRequest{IsWin = false});
                    break;
                case LevelPassedSignal.LevelPassedType.Next:
                    _gameStateMachine.SignalBus.Fire<NextLevelRequest>();
                    break;
                case LevelPassedSignal.LevelPassedType.Win:
                    _gameStateMachine.SignalBus.Fire(new EndGameRequest{IsWin = true});
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Enter()
        {
            base.Enter();
            
            ResetServices();
            
            _gameStateMachine.SignalBus.Fire(new LevelPassedSignal{PassedType = LevelPassedSignal.LevelPassedType.Next});
        }

        private void ResetServices()
        {
            var services = _gameStateMachine.Services.OfType<IResetable>().ToList();

            foreach (var service in services) 
                service.Reset();
        }
    }
}