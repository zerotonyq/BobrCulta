using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Gameplay.Services.Tree.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(TreeServiceConfig), fileName = nameof(TreeServiceConfig))]
    public class TreeServiceConfig : ScriptableObject
    {
        public int initialPartCount;
        
        public float minPartRadius;
        
        public float maxPartRadius;

        public float partHeight;

        public AssetReferenceGameObject partReference;
    }
}