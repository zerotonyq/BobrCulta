using UnityEngine;

namespace Gameplay.Services.Level.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(LevelConfig),
        fileName = nameof(LevelConfig))]
    public class LevelConfig : ScriptableObject
    {
    }
}