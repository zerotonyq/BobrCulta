using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Services.UI.Gameplay.Magic.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(MagicProjectilesUIServiceConfig), fileName = nameof(MagicProjectilesUIService))]
    public class MagicProjectilesUIServiceConfig : ScriptableObject
    {
        public AssetReferenceGameObject projectilesViewReference;
    }
}