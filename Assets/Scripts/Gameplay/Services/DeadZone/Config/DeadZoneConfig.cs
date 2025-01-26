using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Services.DeadZone.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(DeadZoneConfig), fileName = nameof(DeadZoneConfig))]
    public class DeadZoneConfig : ScriptableObject
    {
        public AssetReferenceGameObject deadZoneReference;
        public float deadZoneDimension;
    }
}