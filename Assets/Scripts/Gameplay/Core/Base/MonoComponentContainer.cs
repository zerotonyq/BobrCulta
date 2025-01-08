using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Core.Base
{
    public abstract class MonoComponentContainer : MonoBehaviour
    {
        public abstract Task Initialize();
        public abstract List<MonoComponent> Components { get; }
    }
}