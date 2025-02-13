using Cysharp.Threading.Tasks;
using Gameplay.Core.Character;
using Gameplay.Services.Base;
using Gameplay.Services.DeadZone.Config;
using Signals;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Activate;
using Zenject;

namespace Gameplay.Services.DeadZone
{
    public class DeadZoneService : GameService, IInitializable
    {
        [Inject] private DeadZoneConfig _config;

        private DeadZoneComponent _deadZoneComponent;

        public override void Initialize()
        {
            _signalBus.Subscribe<TreeLevelChangedSignal>(MoveDeadZone);
            base.Initialize();
        }

        private void MoveDeadZone(TreeLevelChangedSignal signal)
        {
            _deadZoneComponent.transform.position = signal.LevelPosition - Vector3.up * _config.verticalOffset;
        }

        public override async void Boot()
        {
            _deadZoneComponent = (await Addressables.InstantiateAsync(_config.deadZoneReference))
                .GetComponent<DeadZoneComponent>();

            _deadZoneComponent.Initialize(_config.deadZoneDimension);

            _deadZoneComponent.ColliderDetected += ProcessDetectedCollider;

            MoveDeadZone(new TreeLevelChangedSignal());

            base.Boot();
        }

        private void ProcessDetectedCollider(Collider coll)
        {
            if(coll.TryGetComponent(out CharacterComponent character))
                character.Die();
            
            if (coll.TryGetComponent(out IActivateable activateable))
                activateable.Deactivate();
        }
    }
}