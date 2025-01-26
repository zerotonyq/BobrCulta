using Signals;

namespace GameState.States
{
    public class BootState : Base.GameState
    {
                
        private readonly int _initializeCount;
        
        private int _currentInitializeCount;
        
        public BootState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
            gameStateMachine.SignalBus.Subscribe<ServiceBootEndRequest>(CheckBootEnd);
            _initializeCount = gameStateMachine.Services.Count;
        }

        private void CheckBootEnd()
        {
            ++_currentInitializeCount;
            
            if(_currentInitializeCount == _initializeCount)
                _gameStateMachine.SetState<LevelState>();
        }

        public override void Enter()
        {
            base.Enter();
            _gameStateMachine.SignalBus.Fire<BootRequest>();
        }
    }
}