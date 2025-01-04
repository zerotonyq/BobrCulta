using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.Camera.Config;
using Signals;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Initialize;
using Zenject;

namespace Gameplay.Camera
{
    public class CameraProvider : IInitializableConcreteConfig<CameraConfig>
    {
        private CinemachineCamera _camera;

        [Inject] private SignalBus _signalBus;

        [Inject]
        public async Task Initialize(CameraConfig config)
        {
            _camera = (await Addressables.InstantiateAsync(config.cameraReference)).GetComponent<CinemachineCamera>();

            _signalBus.Subscribe<PlayerInitializedSignal>(a =>
            {
                SetTarget(a.Player.transform);
                AddLookAt(a.Player.transform);
            });
            _signalBus.Subscribe<NextBossSignal>(a => AddLookAt(a.Boss.transform));

            _camera.Target.CustomLookAtTarget = true;
            _camera.Target.LookAtTarget = _camera.GetComponentInChildren<CinemachineTargetGroup>().transform;
        }

        private void AddLookAt(Transform target)
        {
            var targetGroup = _camera.GetComponentInChildren<CinemachineTargetGroup>(); 
            targetGroup.Targets.Add(new CinemachineTargetGroup.Target {Object = target, Radius =  1, Weight = 1});
        }

        private void SetTarget(Transform target) => _camera.Target.TrackingTarget = target;
    }
}