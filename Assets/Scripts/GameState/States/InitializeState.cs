using Signals;

namespace GameState.States
{
    public class InitializeState : Base.GameState
    {
        private readonly int _initializeCount;
        
        private int _currentInitializeCount;
        
        public InitializeState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
            gameStateMachine.SignalBus.Subscribe<InitializedServiceSignal>(CheckInitializeCount);
            _initializeCount = gameStateMachine.Services.Count;
        }

        private void CheckInitializeCount()
        {
            ++_currentInitializeCount;
            
            if(_currentInitializeCount == _initializeCount)
                _gameStateMachine.SetState<MenuState>();
        }
    }
}