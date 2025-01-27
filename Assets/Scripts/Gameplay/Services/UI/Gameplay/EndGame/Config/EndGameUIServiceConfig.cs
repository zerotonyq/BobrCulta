using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Services.UI.Gameplay.EndGame.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(EndGameUIServiceConfig), fileName = nameof(EndGameUIServiceConfig))]
    public class EndGameUIServiceConfig : ScriptableObject
    {
        public AssetReferenceGameObject endGameViewReference;
        public string winText;
        public string looseText;
    }
}