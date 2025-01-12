using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Services.Player.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(PlayerConfig), fileName = nameof(PlayerConfig))]
    public class PlayerConfig : ScriptableObject
    {
        public AssetReferenceGameObject playerReference;
    }
}