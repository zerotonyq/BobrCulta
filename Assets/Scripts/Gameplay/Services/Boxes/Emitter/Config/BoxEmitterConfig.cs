using System.Collections.Generic;
using Gameplay.Core.Pickup.Base;
using Gameplay.Services.Boxes;
using UnityEngine;

namespace Gameplay.Magic.Boxes.Emitter.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(BoxEmitterConfig), fileName = nameof(BoxEmitterConfig))]
    public class BoxEmitterConfig : ScriptableObject
    {
        public List<Pickupable> pickupables = new();
        public BoxComponent boxPrefab;
    }
}