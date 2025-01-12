using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Services.UI.Menu.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(MenuUIManagerConfig), fileName = nameof(MenuUIManagerConfig))]
    public class MenuUIManagerConfig : ScriptableObject
    {
        public AssetReferenceGameObject menuCanvas;
    }
}