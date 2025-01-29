using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.Services.Base;
using Gameplay.Services.Tree.Config;
using Signals;
using Signals.Activities.Base;
using Signals.Activities.Boss;
using Signals.Level;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.MeshGeneration;
using Utils.Reset;
using Zenject;

namespace Gameplay.Services.Tree
{
    public class TreeService : GameService, IInitializable, IResetable
    {
        [Inject] private TreeServiceConfig _config;

        private Transform _container;

        private int _currentMovePartDownCount;

        private struct TreePart
        {
            public float PartRadius;
            public Transform PartTransform;
            public Transform ConnectorTransform;
        }

        private readonly Queue<TreePart> _treeParts = new();

        private TreePart _lastPart;

        public override void Initialize()
        {
            _signalBus.Subscribe<NextLevelRequest>(RebuildNextTreePartFromBuffer);

            base.Initialize();
        }

        public void Reset()
        {
            foreach (var treePart in _treeParts)
            {
                treePart.PartTransform.SetPositionAndRotation(
                    treePart.PartTransform.position +
                    new Vector3(0, _config.partHeight * 2 * _currentMovePartDownCount, 0), Quaternion.identity);
                treePart.ConnectorTransform.SetPositionAndRotation(
                    treePart.ConnectorTransform.position +
                    new Vector3(0, _config.partHeight * 2 * _currentMovePartDownCount, 0), Quaternion.identity);
            }

            _currentMovePartDownCount = 0;
        }

        public override async void Boot()
        {
            _container = new GameObject("TreePartsContainer").transform;

            _container.transform.position = Vector3.zero;

            await BuildPartsAndConnectors();

            base.Boot();
        }

        private async Task BuildPartsAndConnectors()
        {
            for (var i = 0; i < _config.initialPartCount; i++)
            {
                var radius = Random.Range(_config.minPartRadius, _config.maxPartRadius);

                var part = await CreateTreePart(radius, radius);

                var connector = await CreateTreePart(_lastPart.PartRadius, radius, "Connector");

                MovePartDown(part, 2);
                MovePartDown(connector);

                _lastPart = new TreePart
                {
                    PartRadius = radius,
                    ConnectorTransform = connector,
                    PartTransform = part
                };

                _treeParts.Enqueue(_lastPart);

                await Task.Delay(2);
            }
        }

        private void RebuildNextTreePartFromBuffer()
        {
            if (!_treeParts.TryDequeue(out var part))
            {
                Debug.Log("cannot dequeue tree part from buffer");
                return;
            }

            part.ConnectorTransform.gameObject.SetActive(true);

            var nextPart = _treeParts.Peek();

            _signalBus.Fire(new TreeLevelChangedSignal
            {
                LevelPosition = nextPart.PartTransform.position + new Vector3(0, _config.partHeight / 2, 0),
                Radius = nextPart.PartRadius
            });

            nextPart.ConnectorTransform.gameObject.SetActive(false);

            var radius = Random.Range(_config.minPartRadius, _config.maxPartRadius);

            var mesh = TruncatedConeMeshGenerator.Generate(radius, radius, _config.partHeight, 21);
            var connectorMesh =
                TruncatedConeMeshGenerator.Generate(_lastPart.PartRadius, radius, _config.partHeight, 21);

            UpdateMeshAndCollider(part.PartTransform.gameObject, mesh);
            UpdateMeshAndCollider(part.ConnectorTransform.gameObject, connectorMesh);

            part.PartRadius = radius;

            _treeParts.Enqueue(part);

            MovePartDown(part.PartTransform, 2);
            MovePartDown(part.ConnectorTransform);

            _lastPart = part;

            ++_currentMovePartDownCount;
        }

        private async Task<Transform> CreateTreePart(float radiusTop, float radiusBottom, string name = "TreePart")
        {
            var part = await Addressables.InstantiateAsync(_config.partReference);

            part.transform.SetParent(_container);

            part.name = name + "_" + _treeParts.Count;

            var mesh = TruncatedConeMeshGenerator.Generate(radiusTop, radiusBottom, _config.partHeight, 21);

            UpdateMeshAndCollider(part, mesh);

            return part.transform;
        }

        private static void UpdateMeshAndCollider(GameObject part, Mesh mesh)
        {
            part.GetComponent<MeshFilter>().sharedMesh = mesh;
            part.GetComponent<MeshCollider>().sharedMesh = mesh;
        }

        private void MovePartDown(Transform partTransform, int heightCount = 1)
        {
            if (_lastPart.PartTransform == null)
                partTransform.transform.position = Vector3.zero;
            else
                partTransform.transform.position =
                    _lastPart.PartTransform.position - new Vector3(0, _config.partHeight * heightCount, 0);
        }

        private void MovePartUp(Transform part, int heightCount)
        {
            part.position += new Vector3(0, _config.partHeight * heightCount, 0);
        }
    }
}