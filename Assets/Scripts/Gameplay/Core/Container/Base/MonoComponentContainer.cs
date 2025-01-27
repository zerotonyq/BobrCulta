using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Core.Base;
using UnityEngine;

namespace Gameplay.Core.Container.Base
{
    public abstract class MonoComponentContainer : MonoBehaviour
    {
        public abstract Task Initialize();
        public abstract List<MonoComponent> Components { get; }
    }
}