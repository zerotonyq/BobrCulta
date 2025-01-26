using Gameplay.Magic.Boxes;
using Gameplay.Magic.Boxes.Emitter.Config;
using Gameplay.Services.Base;
using Signals;
using UnityEngine;
using Utils.Pooling;
using Zenject;

namespace Gameplay.Services.Boxes.Emitter
{
    public class BoxEmitter : GameService, IInitializable
    {
        [Inject] private BoxEmitterConfig _config;

        private Transform _container;

        private float _radius;
        private Vector3 _position;

        public override void Initialize()
        {
            _signalBus.Subscribe<TreeLevelChangedSignal>(ChangeEmissionField);
            _signalBus.Subscribe<NextBossSignal>(EmitBoxes);

            base.Initialize();
        }

        public override void Boot()
        {
            _container = new GameObject("BoxContainer").transform;
            _container.transform.position = Vector3.zero;
            base.Boot();
        }

        private void EmitBoxes()
        {
            for (var i = 0; i < 5; ++i)
            {
                var position = _position +
                               new Vector3(Random.Range(-_radius, _radius), 1, Random.Range(-_radius, _radius));

                CreateBox(position);
            }
        }

        private void CreateBox(Vector3 position)
        {
            var box = PoolManager.GetFromPool(_config.boxPrefab.GetType(), _config.boxPrefab.gameObject)
                .GetComponent<BoxComponent>();

            box.transform.SetParent(_container);
            
            box.Initialize(_config.pickupables);

            box.Activate(position);
        }

        private void ChangeEmissionField(TreeLevelChangedSignal signal)
        {
            _radius = signal.Radius;
            _position = signal.LevelPosition;
        }
    }
}