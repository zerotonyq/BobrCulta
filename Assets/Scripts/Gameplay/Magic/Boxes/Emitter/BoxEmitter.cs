using Gameplay.Magic.Boxes.Emitter.Config;
using Gameplay.Services.Base;
using Signals;
using UnityEngine;
using Utils.Pooling;
using Zenject;

namespace Gameplay.Magic.Boxes
{
    public class BoxEmitter : GameService
    {
        [Inject] private BoxEmitterConfig _config;

        private float _radius;
        private Vector3 _position;

        public override void Initialize()
        {
            _signalBus.Subscribe<TreeLevelChangedSignal>(ChangeEmissionField);
            _signalBus.Subscribe<NextBossSignal>(EmitBoxes);
            base.Initialize();
        }

        private void EmitBoxes()
        {
            //TODO здесь i меняем на рандомное кол-во в пределах от min до max.
            //можно попробовать сделать тоже из секции difficulty, где будем для каджой секции настраивать кол-во либо
            //в пределах и рандомить, либо ставить конкретное кол-во
            for (int i = 0; i < 5; i++)
            {
                var position = _position +
                               new Vector3(Random.Range(-_radius, _radius), 1, Random.Range(-_radius, _radius));
                
                CreateBox(position);
            }
        }

        public void CreateBox(Vector3 position)
        {
            var box = PoolManager.GetFromPool(_config.boxPrefab.GetType(), _config.boxPrefab.gameObject)
                .GetComponent<BoxComponent>();

            // TODO нам нужно знать, откуда брать pickupables
            //короче делаем difficulty менеджер, который будет постепенно добавлять pickupables.
            //и просто обращаемся к его статическому полю с текущими pickupables и пихаем сюда
            //box.Initialize();

            box.transform.position = position;
        }

        private void ChangeEmissionField(TreeLevelChangedSignal signal)
        {
            _radius = signal.Radius;
            _position = signal.LevelPosition;
        }
    }
}