using Modifiers.Base;
using UnityEngine;

namespace Modifiers.Velocity.Base
{
    public abstract class VelocityModifier : Modifier
    {
        public abstract Vector3 Apply(Vector3 velocity);
    }
}