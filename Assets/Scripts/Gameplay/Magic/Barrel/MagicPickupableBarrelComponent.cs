using System;
using System.Collections.Generic;
using Gameplay.Core.Base;
using Gameplay.Core.Pickup;
using Gameplay.Core.Pickup.Base;
using Gameplay.Magic.Abilities.Base.Pickupable;
using Gameplay.Magic.Barrel.Enum;
using Input;
using R3;
using UnityEngine;
using Utils;
using Utils.ObservableExtension;
using Observable = R3.Observable;

namespace Gameplay.Magic.Barrel
{
    public class MagicPickupableBarrelComponent : MonoComponent
    {
        [SerializeField] private Transform rotateBarrel;
        [SerializeField] private Transform rotationOffset;
        [SerializeField] private int sectorsCount;
        [SerializeField] private float rotateSpeed;
        private float SectorAngle => 360.0f / sectorsCount;

        public Action<MagicTypeArgs> MagicPickupableProvided;

        [SerializeField] private List<MagicPickupableBarrelPosition> _barrelPositions;
        private DisposableBag _disposableBag;

        [Serializable]
        public struct MagicTypeArgs
        {
            public Pickupable Pickupable;
            public ApplicationType ApplicationType;

            public MagicTypeArgs(Pickupable pickupable, ApplicationType applicationType)
            {
                Pickupable = pickupable;
                ApplicationType = applicationType;
            }
        }

        public override void Initialize()
        {
            GetComponentInParent<PickupComponent>().PickedUp += TryInsertPickupable;

            InstantiateProjectilePositions();

            InitializeRotationOffset();

            var subscription1 = Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ => Rotate());

            var subscription2 = InputProvider.InputSystemActions.Player.Fire.ToObservablePerformed()
                .Subscribe(ctx => ChooseProjectile(ApplicationType.Fire));

            var subscription3 = InputProvider.InputSystemActions.Player.ApplyToItself.ToObservablePerformed()
                .Subscribe(ctx => ChooseProjectile(ApplicationType.ApplyToItself));

            _disposableBag.Add(subscription1);
            _disposableBag.Add(subscription2);
            _disposableBag.Add(subscription3);
        }

        private void InstantiateProjectilePositions()
        {
            _barrelPositions = new List<MagicPickupableBarrelPosition>();

            var offset = 180 + SectorAngle;
            for (var i = 0; i < sectorsCount; ++i)
            {
                var angleDegrees = i * (360.0f / sectorsCount) + offset;

                var projectilePosition =
                    new GameObject("barrelPosition_" + i).AddComponent<MagicPickupableBarrelPosition>();

                projectilePosition.gameObject.layer = LayerMask.NameToLayer("Magic");

                projectilePosition.transform.SetParent(rotateBarrel);

                projectilePosition.transform.SetPositionAndRotation(
                    VectorUtility.GetPointOnCircle(-angleDegrees, rotateBarrel.position,
                        rotateBarrel.GetComponent<MeshFilter>().mesh.bounds.size.x / 2.0f, rotateBarrel.up,
                        rotateBarrel.right),
                    Quaternion.identity);

                projectilePosition.transform.localRotation = Quaternion.AngleAxis(angleDegrees, rotateBarrel.up);

                _barrelPositions.Add(projectilePosition);
            }
        }

        private void InitializeRotationOffset()
        {
            var rotationOffsetAngle = (360.0f / sectorsCount / 4) * Mathf.Deg2Rad;
            rotationOffset.transform.localRotation = new Quaternion(
                0,
                Mathf.Sin(rotationOffsetAngle),
                0,
                Mathf.Cos(rotationOffsetAngle));
        }

        private void TryInsertPickupable(IPickupable pickupable)
        {
            if (pickupable is not MagicPickupable magicPickupable)
                return;

            var tile = _barrelPositions[GetCurrentSector()];

            tile.Set(magicPickupable);
        }

        private void ChooseProjectile(ApplicationType type)
        {
            var barrelPosition = _barrelPositions[GetCurrentSector()];

            if (barrelPosition.Pickupable == null)
                return;

            MagicPickupableProvided?.Invoke(new MagicTypeArgs(barrelPosition.Pickupable, type));

            barrelPosition.Pickupable.Deactivate();

            barrelPosition.Set(null);
        }

        private int GetCurrentSector()
        {
            var angle = rotateBarrel.localRotation.eulerAngles.y;

            return (int)(angle / SectorAngle);
        }

        private void Rotate()
        {
            var rotor = new Quaternion(0, Mathf.Sin(rotateSpeed * Mathf.Deg2Rad), 0,
                Mathf.Cos(rotateSpeed / 100 * Mathf.Deg2Rad));

            rotateBarrel.rotation *= rotor;
        }

        public override void Reset()
        {
            foreach (var barrelPosition in _barrelPositions)
                barrelPosition.Set(null);
        }

        private void OnDestroy() => _disposableBag.Dispose();
    }
}