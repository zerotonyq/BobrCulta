using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Boss.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(BossServiceConfig), fileName = nameof(BossServiceConfig))]
    public class BossServiceConfig : ScriptableObject
    {
        public List<AssetReferenceGameObject> bossesReferences;
    }
}