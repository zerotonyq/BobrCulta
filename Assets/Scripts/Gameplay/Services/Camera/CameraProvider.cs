using Cysharp.Threading.Tasks;
using Gameplay.Services.Base;
using Gameplay.Services.Camera.Config;
using Signals;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.Services.Camera
{
    public class CameraProvider : GameService, IInitializable
    {
        private CinemachineCamera _camera;


        [Inject] private CameraConfig _cameraConfig;

        public override async void Initialize()
        {
            _camera = (await Addressables.InstantiateAsync(_cameraConfig.cameraReference))
                .GetComponent<CinemachineCamera>();

            _signalBus.Subscribe<PlayerInitializedSignal>(a =>
            {
                SetTarget(a.Player.transform);
                AddLookAt(a.Player.transform);
            });

            _signalBus.Subscribe<NextBossSignal>(a => { AddLookAt(a.Boss.transform); });

            _camera.Target.CustomLookAtTarget = true;
            _camera.Target.LookAtTarget = _camera.GetComponentInChildren<CinemachineTargetGroup>().transform;

            base.Initialize();
        }

        private void AddLookAt(Transform target)
        {
            var targetGroup = _camera.GetComponentInChildren<CinemachineTargetGroup>();
            targetGroup.Targets.Add(new CinemachineTargetGroup.Target { Object = target, Radius = 2, Weight = 2 });
        }

        private void SetTarget(Transform target) => _camera.Target.TrackingTarget = target;
    }
}