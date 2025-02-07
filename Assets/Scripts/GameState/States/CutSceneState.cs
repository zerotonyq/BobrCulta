using System;
using Signals.Cutscenes;

namespace GameState.States
{
    public class CutSceneState: Base.GameState
    {
        public CutSceneState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
            gameStateMachine.SignalBus.Subscribe<CutsceneEndSignal>(OnCutsceneEnd);
        }

        private void OnCutsceneEnd(CutsceneEndSignal obj)
        {
            switch (obj.Type)
            {
                case CutsceneEndSignal.CutsceneType.None:
                    break;
                case CutsceneEndSignal.CutsceneType.Start:
                    _gameStateMachine.SetState<BootState>();
                    break;
                case CutsceneEndSignal.CutsceneType.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}