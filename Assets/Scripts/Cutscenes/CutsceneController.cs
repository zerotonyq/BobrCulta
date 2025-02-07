using Gameplay.Services.Base;
using Signals.Cutscenes;
using Signals.GameStates;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

namespace Cutscenes
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CutsceneController : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;

        private PlayableDirector _playableDirector;

        [Inject]
        public void Initialize()
        {
            _signalBus.Subscribe<StartGameRequest>(StartCutscene);
            _playableDirector = GetComponent<PlayableDirector>();
        }

        private void StartCutscene() => _playableDirector.Play();

        public void OnStartCutsceneEnded()
        {
            _signalBus.Fire(new CutsceneEndSignal(CutsceneEndSignal.CutsceneType.Start));
        }
    }
}