using System;
using Gameplay.Player;
using Gameplay.Player.Config;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Gameplay.EntryPoint.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(GameplayEntryPointConfig),
        fileName = nameof(GameplayEntryPointConfig))]
    public class GameplayEntryPointConfig : Utils.Initialize.Config
    {
        public AssetReferenceGameObject playerAssetReference;
        public CharacterConfig playerConfig;

        public override Type InitializableType { get; } = typeof(GameplayEntryPoint);
    }
}