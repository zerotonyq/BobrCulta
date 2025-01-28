using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Services.Base;
using Gameplay.Services.Boxes.Emitter.Config;
using Signals;
using Signals.Chapter;
using Signals.GameStates;
using Signals.Level;
using UnityEngine;
using Utils.Coroutines;
using Utils.Pooling;
using Utils.Reset;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Services.Boxes.Emitter
{
    public class BoxEmitter : GameService, IInitializable, IResetable
    {
        [Inject] private BoxEmitterConfig _config;

        private CoroutineExecutor _coroutineExecutor;
        private Coroutine _currentEmissionCoroutine;
        private YieldInstruction _currentWaitInstruction;
        
        private float _currentEmissionPeriod;
        
        private Transform _container;

        private float _radius;
        private Vector3 _position;

        private List<BoxComponent> _boxes = new();

        public override void Initialize()
        {
            _signalBus.Subscribe<TreeLevelChangedSignal>(ChangeEmissionField);
            _signalBus.Subscribe<NextLevelRequest>(EmitBoxes);
            _signalBus.Subscribe<EndGameRequest>(StopEmission);
            _signalBus.Subscribe<NextChapterRequest>(UpdateEmissionPeriod);
            
            _currentEmissionPeriod = _config.emissionPeriod;
            _currentWaitInstruction = new WaitForSeconds(_currentEmissionPeriod);
            
            base.Initialize();
        }

        public override void Boot()
        {
            _coroutineExecutor =
                new GameObject(nameof(BoxEmitter) + "_CoroutineExecutor").AddComponent<CoroutineExecutor>();
            _container = new GameObject("BoxContainer").transform;
            _container.transform.position = Vector3.zero;
            base.Boot();
        }

        private void EmitBoxes()
        {
            StopEmission();
            
            _currentEmissionCoroutine = _coroutineExecutor.Add(EmissionCoroutine());
        }

        private IEnumerator EmissionCoroutine()
        {
            while (true)
            {
                for (var i = 0; i < 5; ++i)
                {
                    var position = _position +
                                   new Vector3(Random.Range(-_radius, _radius), 5, Random.Range(-_radius, _radius));

                    CreateBox(position);
                }
                yield return _currentWaitInstruction;
            }
        }
        
        private void CreateBox(Vector3 position)
        {
            var box = PoolManager.GetFromPool(_config.boxPrefab.GetType(), _config.boxPrefab.gameObject)
                .GetComponent<BoxComponent>();

            box.Reset();
            
            box.transform.SetParent(_container);
            
            box.Initialize(_config.pickupables);

            box.Activate(position);
            
            _boxes.Add(box);
        }
        
        private void UpdateEmissionPeriod()
        {
            _currentEmissionPeriod += _config.emissionPeriodIncreaseFactor;
            _currentWaitInstruction = new WaitForSeconds(_currentEmissionPeriod);
        }
        
        private void ChangeEmissionField(TreeLevelChangedSignal signal)
        {
            _radius = signal.Radius;
            _position = signal.LevelPosition;
        }
        
        private void StopEmission()
        {
            if(_currentEmissionCoroutine != null)
                _coroutineExecutor.Remove(_currentEmissionCoroutine);

            _currentEmissionCoroutine = null;
        }

        public void Reset()
        {
            StopEmission();
            foreach (var box in _boxes) box.Deactivate();
            _currentEmissionPeriod = _config.emissionPeriod;
            _currentWaitInstruction = new WaitForSeconds(_currentEmissionPeriod);
        }
    }
}