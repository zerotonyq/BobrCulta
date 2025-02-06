using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay.Core.Container;
using Gameplay.Magic.Abilities;
using Gameplay.Services.Base;
using Gameplay.Services.Camera.Config;
using Signals;
using Signals.Activities.Boss;
using Signals.GameStates;
using Signals.Level;
using Signals.Player;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Gameplay.Services.Camera
{
    public class CameraProvider : GameService, IInitializable
    {
        private CinemachineCamera _camera;
        
        private Transform _defaultTarget;
        
        [Inject] private CameraConfig _cameraConfig;

        public override async void Initialize()
        {
            _camera = (await Addressables.InstantiateAsync(_cameraConfig.cameraReference))
                .GetComponent<CinemachineCamera>();
      
            
            _defaultTarget = (await Addressables.InstantiateAsync(_cameraConfig.defaultTarget)).transform;
                
            _signalBus.Subscribe<EndGameRequest>(SetDefaultTarget);
            _signalBus.Subscribe<StartGameRequest>(RemoveDefaultTarget);
            
            _signalBus.Subscribe<PlayerInitializedSignal>(a =>
            {
                SetOverlayCamera(a.Player);
                SetTarget(a.Player.transform);
                AddLookAt(a.Player.transform);
            });

            _signalBus.Subscribe<BossObtainedSignal>(a => { AddLookAt(a.Boss.transform); });

            _camera.Target.CustomLookAtTarget = true;
            _camera.Target.LookAtTarget = _camera.GetComponentInChildren<CinemachineTargetGroup>().transform;
            
            SetDefaultTarget();

            base.Initialize();
        }


        private void SetOverlayCamera(ComponentContainer container)
        {
            var camera = container.GetComponentInChildren<UnityEngine.Camera>();
            if (!camera)
                return;

            var additionalCameraData = UnityEngine.Camera.main.GetUniversalAdditionalCameraData();
            additionalCameraData.cameraStack.Add(camera);
        }

        public override void Boot()
        {
            RemoveDefaultTarget();
            base.Boot();
        }
        
        private void SetDefaultTarget()
        {
            SetTarget(_defaultTarget);
            AddLookAt(_defaultTarget);
        }
        private void RemoveDefaultTarget() => TryRemoveLookAt(_defaultTarget);

        private void SetTarget(Transform target) => _camera.Target.TrackingTarget = target;

        private void AddLookAt(Transform target)
        {
            var targetGroup = _camera.GetComponentInChildren<CinemachineTargetGroup>();
            targetGroup.Targets.Add(new CinemachineTargetGroup.Target { Object = target, Radius = 2, Weight = 2 });
        }
        
        private void TryRemoveLookAt(Transform target)
        {
            var targetGroup = _camera.GetComponentInChildren<CinemachineTargetGroup>();
            var deleteTarget = targetGroup.Targets.Find(a => a.Object == target);
            
            if (deleteTarget == null)
                return;
            
            targetGroup.Targets.Remove(deleteTarget);
        }
    }
}