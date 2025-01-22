using UnityEngine;

namespace Gameplay.Magic.Boxes.Emitter.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(BoxEmitterConfig), fileName = nameof(BoxEmitterConfig))]
    public class BoxEmitterConfig : ScriptableObject
    {
        public BoxComponent boxPrefab;
    }
}