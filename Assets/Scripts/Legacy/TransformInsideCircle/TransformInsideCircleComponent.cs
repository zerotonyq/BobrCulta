using R3;
using UnityEngine;

namespace Gameplay.Core
{
    public class TransformInsideCircleComponent : MonoBehaviour
    {
        [field: SerializeField] public int Radius { get; private set; }
        [field: SerializeField] public Transform ControlledTransform { get; private set; }
        public Vector3 Center => transform.position;
        public bool IsActive { get; set; }


        public void UpdateControlledTransformPosition(Vector3 position)
        {
            if (!IsActive)
                return;
            if (ControlledTransform != null) ControlledTransform.position = position;
        }
    }
}