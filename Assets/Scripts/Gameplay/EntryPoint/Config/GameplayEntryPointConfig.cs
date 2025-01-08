using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.EntryPoint.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(GameplayEntryPointConfig),
        fileName = nameof(GameplayEntryPointConfig))]
    public class GameplayEntryPointConfig : ScriptableObject
    {
        public AssetReferenceGameObject playerAssetReference;
        public AssetReferenceGameObject magicProjectilesUIManager;
    }
}