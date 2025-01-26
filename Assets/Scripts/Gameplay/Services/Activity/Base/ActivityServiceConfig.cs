using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Services.Activity.Base
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(ActivityServiceConfig),
        fileName = nameof(ActivityServiceConfig))]
    public class ActivityServiceConfig : ScriptableObject
    {
        public List<ActivityConfig> activityConfigs = new();
    }
}