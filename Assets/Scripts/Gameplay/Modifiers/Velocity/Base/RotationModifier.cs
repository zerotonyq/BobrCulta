using Modifiers.Base;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Gameplay.Modifiers.Velocity.Base
{
    public abstract class RotationModifier : Modifier
    {
        public abstract Quaternion Apply(Quaternion rotation, Vector3 position);
    }
}