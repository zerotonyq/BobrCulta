using System;
using System.Collections.Generic;
using Gameplay.Magic;
using Input;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Utils.ObservableExtension;
using Observable = R3.Observable;

namespace Gameplay.Services.UI.Magic.Views
{
    [RequireComponent(typeof(Canvas))]
    public class MagicProjectilesUIView : MonoBehaviour
    {
        [SerializeField] private Image rotateCircle;
        [SerializeField] private RectTransform rotationOffset;
        [SerializeField] private int sectorsCount;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private ProjectileUIElement projectileUIElementPrefab;

        public Action<Type> MagicTypeProvided;
        public Action<Type> MagicTypeRemoved;

        private DisposableBag _disposableBag;

        private List<ProjectileUIElement> _uiTiles;

        public ReactiveProperty<int> CurrentSector = new();

        public void Initialize()
        {
            InstantiateTiles();

            InitializeRotationOffset();

            GetComponent<Canvas>().worldCamera = UnityEngine.Camera.main;

            var subscription1 = Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ => Rotate());

            var subscription2 = InputProvider.InputSystemActions.Player.Attack.ToObservablePerformed()
                .Subscribe(ctx => ChooseProjectile());

            _disposableBag.Add(subscription1);
            _disposableBag.Add(subscription2);
        }

        private void InstantiateTiles()
        {
            _uiTiles = new List<ProjectileUIElement>();

            for (var i = sectorsCount - 1; i >= 0; --i)
            {
                var angle = i * (360.0f / sectorsCount / 2) * Mathf.Deg2Rad;

                var uiTile = Instantiate(projectileUIElementPrefab, Vector3.zero, Quaternion.identity, rotateCircle.transform);

                uiTile.transform.localPosition = Vector3.zero;

                uiTile.transform.localRotation = rotationOffset.transform.localRotation = new Quaternion(
                    0,
                    0,
                    Mathf.Sin(angle),
                    Mathf.Cos(angle));

                uiTile.name += _uiTiles.Count;
                
                _uiTiles.Add(uiTile);
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
            var current = GetTileByAngle(90);

            if(current.Type != null)
                MagicTypeRemoved?.Invoke(current.Type);
            
            current.Set(magicPickupable.projectileUISprite, magicPickupable.magicAbilityPrefab.GetType());
        }

        private void ChooseProjectile()
        {
            var tile = GetTileByAngle(90);

            if (tile.Type == null)
            {
                Debug.LogWarning("No tile at this sector");
                return;
            }

            MagicTypeProvided?.Invoke(tile.Type);
            
            tile.Clear();
        }

        private ProjectileUIElement GetTileByAngle(float angleCounterClockwise) => _uiTiles[GetCurrentSector(angleCounterClockwise)];

        private int GetCurrentSector(float angleOffsetCounterClockwise = 0f)
        {
            var q = rotateCircle.transform.localRotation;

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

            rotateCircle.transform.rotation *= rotor;

            CurrentSector.Value = GetCurrentSector();
        }

        public void OnMagicProjectileProvided(MagicPickupable magicPickupable) => TryInsertProjectile(magicPickupable);

        private void OnDestroy() => _disposableBag.Dispose();
    }
}