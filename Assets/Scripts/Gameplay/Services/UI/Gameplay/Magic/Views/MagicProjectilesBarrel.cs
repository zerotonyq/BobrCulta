using System;
using System.Collections.Generic;
using Gameplay.Core.Base;
using Gameplay.Magic.Abilities.Base.Pickupable;
using Gameplay.Services.UI.Gameplay.Magic.Elements;
using Gameplay.Services.UI.Gameplay.Magic.Enum;
using Input;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Utils.ObservableExtension;
using Observable = R3.Observable;

namespace Gameplay.Services.UI.Gameplay.Magic.Views
{
    [RequireComponent(typeof(Canvas))]
    public class MagicProjectilesBarrel : MonoComponent
    {
        [SerializeField] private Transform rotateBarrel;
        [SerializeField] private Transform rotationOffset;
        [SerializeField] private int sectorsCount;
        [SerializeField] private float rotateSpeed;

        public Action<MagicTypeArgs> MagicTypeProvided;
        public Action<MagicTypeArgs> MagicTypeRemoved;

        private DisposableBag _disposableBag;

        [SerializeField] private List<Transform> _projectilePositions;

        public ReactiveProperty<int> CurrentSector = new();

        public struct MagicTypeArgs
        {
            public readonly Type MagicType;
            public readonly ApplicationType ApplicationType;

            public MagicTypeArgs(Type magicType, ApplicationType applicationType)
            {
                MagicType = magicType;
                ApplicationType = applicationType;
            }
        }

        public override void Initialize()
        {
            InstantiateProjectilePositions();

            InitializeRotationOffset();

            GetComponent<Canvas>().worldCamera = UnityEngine.Camera.main;

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
            _projectilePositions = new List<Transform>();

            for (var i = sectorsCount - 1; i >= 0; --i)
            {
                var angle = i * (360.0f / sectorsCount / 2) * Mathf.Deg2Rad;

                var projectilePosition = new GameObject("projectilePosition_" + i);
                
                projectilePosition.transform.SetParent(rotateBarrel);
                
                projectilePosition.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

                projectilePosition.transform.localRotation = rotationOffset.transform.localRotation = new Quaternion(
                    0,
                    0,
                    Mathf.Sin(angle),
                    Mathf.Cos(angle));


                _projectilePositions.Add(projectilePosition.transform);
            }
        }

        private void InitializeRotationOffset()
        {
            var rotationOffsetAngle = (360.0f / sectorsCount / 4) * Mathf.Deg2Rad;
            rotationOffset.transform.localRotation = new Quaternion(
                0,
                0,
                Mathf.Sin(rotationOffsetAngle),
                Mathf.Cos(rotationOffsetAngle));
        }

        private void TryInsertProjectile(MagicPickupable magicPickupable)
        {
            var tile = GetProjectilePositionByAngle(90);

            if (tile.Type != null)
                MagicTypeRemoved?.Invoke(new MagicTypeArgs(tile.Type, ApplicationType.None));

            tile.Set(magicPickupable.projectileUISprite, magicPickupable.magicAbilityPrefab.GetType());
        }

        private void ChooseProjectile(ApplicationType type)
        {
            var tile = GetProjectilePositionByAngle(90);

            if (tile.Type == null)
            {
                Debug.LogWarning("No tile at this sector");
                return;
            }

            MagicTypeProvided?.Invoke(new MagicTypeArgs(tile.Type, type));

            tile.Clear();
        }

        //TODO: create class 
        private ProjectilePosition GetProjectilePositionByAngle(float angleCounterClockwise) =>
            _projectilePositions[GetCurrentSector(angleCounterClockwise)];

        private int GetCurrentSector(float angleOffsetCounterClockwise = 0f)
        {
            var q = rotateBarrel.localRotation;

            var angle = 2 * Mathf.Atan2(q.z, q.w) * Mathf.Rad2Deg;

            var angleSign = Mathf.Sign(angle);

            angle = 360 * (angleSign > 0 ? 0 : 1) + angle;

            var sector = (int)((angle + (360 - angleOffsetCounterClockwise)) / (360.0f / sectorsCount)) % sectorsCount;

            return sector;
        }

        private void Rotate()
        {
            var rotor = new Quaternion(0, 0, Mathf.Sin(rotateSpeed * Mathf.Deg2Rad),
                Mathf.Cos(rotateSpeed / 100 * Mathf.Deg2Rad));

            rotateBarrel.rotation *= rotor;

            CurrentSector.Value = GetCurrentSector();
        }

        public void OnMagicProjectileProvided(MagicPickupable magicPickupable) => TryInsertProjectile(magicPickupable);

        private void OnDestroy() => _disposableBag.Dispose();

        public override void Reset()
        {
            foreach (var uiTile in _uiTiles) uiTile.Clear();
        }
    }
}