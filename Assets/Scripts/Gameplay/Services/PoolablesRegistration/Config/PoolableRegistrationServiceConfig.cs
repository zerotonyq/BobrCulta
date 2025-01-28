using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Services.PoolablesRegistration.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(PoolableRegistrationServiceConfig), fileName = nameof(PoolableRegistrationServiceConfig))]
    public class PoolableRegistrationServiceConfig : ScriptableObject
    {
        public List<MonoBehaviour> prefabs = new();
    }
}