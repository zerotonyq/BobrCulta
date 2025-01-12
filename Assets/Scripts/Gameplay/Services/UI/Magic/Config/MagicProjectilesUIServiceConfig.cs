using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Services.UI.Magic.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(MagicProjectilesUIServiceConfig), fileName = nameof(MagicProjectilesUIService))]
    public class MagicProjectilesUIServiceConfig : ScriptableObject
    {
        public AssetReferenceGameObject projectilesViewReference;
    }
}