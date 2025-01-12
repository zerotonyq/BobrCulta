using UnityEngine;

namespace GameState.Base
{
    public abstract class GameState
    {
        protected GameStateMachine _gameStateMachine;

        public GameState(GameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;

        public virtual void Exit()
        {
            Debug.Log("exited " + GetType());
        }

        public virtual void Enter()
        {
            Debug.Log("entered " + GetType());
        }
    }
}