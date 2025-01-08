using System.Collections.Generic;
using Gameplay.Core.Base;
using UnityEngine;

namespace Gameplay.Core.ComponentContainer.Base
{
    public interface IComponentContainer
    {
        List<MonoComponent> Components { get; }
        Vector3 GetPosition();
        Transform Transform { get; }

        void Destroy();
    }
}