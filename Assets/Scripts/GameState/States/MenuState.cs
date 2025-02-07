using Signals;
using Signals.GameStates;
using Signals.Menu;

namespace GameState.States
{
    public class MenuState : Base.GameState
    {
        public MenuState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
            gameStateMachine.SignalBus.Subscribe<StartGameRequest>(_gameStateMachine.SetState<CutSceneState>);
        }

        public override void Enter()
        {
            _gameStateMachine.SignalBus.Fire<MenuRequestSignal>();
            base.Enter();
        }
    }
}