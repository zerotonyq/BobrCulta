using System;
using System.Collections.Generic;
using Gameplay.Magic.Pickupables.Base;
using Input;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Utils.ObservableExtension;
using Observable = R3.Observable;

namespace UI.Magic
{
    public class MagicProjectilesUIManager : MonoBehaviour
    {
        [SerializeField] private Image rotateCircle;
        [SerializeField] private RectTransform rotationOffset;
        [SerializeField] private int sectorsCount;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private UITile uiTilePrefab;

        public Action<Type> MagicTypeProvided;
        public Action<Type> MagicTypeRemoved;

        private DisposableBag _disposableBag;

        private List<UITile> _uiTiles;

        private Queue<MagicPickupable> _bufferProjectiles = new();

        public ReactiveProperty<int> CurrentSector = new();

        public void Initialize()
        {
            InstantiateTiles();

            InitializeRotationOffset();

            GetComponent<Canvas>().worldCamera = Camera.main;

            var subscription1 = Observable.EveryUpdate(UnityFrameProvider.Update).Subscribe(_ => Rotate());

            var subscription2 = InputProvider.InputSystemActions.Player.Attack.ToObservablePerformed()
                .Subscribe(ctx => ChooseProjectile());

            var subscription3 = InputProvider.InputSystemActions.Player.InsertMagicProjectile.ToObservablePerformed()
                .Subscribe(ctx => TryInsertProjectile());

            _disposableBag.Add(subscription1);
            _disposableBag.Add(subscription2);
            _disposableBag.Add(subscription3);
        }

        private void InstantiateTiles()
        {
            _uiTiles = new List<UITile>();

            for (var i = sectorsCount - 1; i >= 0; --i)
            {
                var angle = i * (360.0f / sectorsCount / 2) * Mathf.Deg2Rad;

                var uiTile = Instantiate(uiTilePrefab, Vector3.zero, Quaternion.identity, rotateCircle.transform);

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

        private void TryInsertProjectile()
        {
            var current = GetTileByAngle(150);
            
            if (!_bufferProjectiles.TryDequeue(out var magicPickupable))
            {
                Debug.LogWarning("There is no UI tiles in buffer");
                return;
            }

            if(current.Type != null)
                MagicTypeRemoved?.Invoke(current.Type);
            current.Set(magicPickupable.projectileUISprite, magicPickupable.MagicProjectilePrefab.GetType());
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

        private UITile GetTileByAngle(float angleCounterClockwise) => _uiTiles[GetCurrentSector(angleCounterClockwise)];

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
            var rotor = new Quaternion(0, 0, Mathf.Sin(rotateSpeed / 100 * Mathf.Deg2Rad),
                Mathf.Cos(rotateSpeed / 100 * Mathf.Deg2Rad));

            rotateCircle.transform.rotation *= rotor;

            CurrentSector.Value = GetCurrentSector();
        }

        public void OnMagicProjectileProvided(MagicPickupable magicPickupable) => _bufferProjectiles.Enqueue(magicPickupable);

        private void OnDestroy() => _disposableBag.Dispose();
    }
}