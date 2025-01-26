using Signals;
using Signals.GameStates;

namespace GameState.States
{
    public class MenuState : Base.GameState
    {
        public MenuState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
            gameStateMachine.SignalBus.Subscribe<StartGameRequest>(_gameStateMachine.SetState<BootState>);
        }
    }
}