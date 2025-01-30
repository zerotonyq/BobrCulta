using System.Collections.Generic;
using Gameplay.Core.Pickup.Base;
using UnityEngine;

namespace Gameplay.Services.Boxes.Emitter.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(BoxEmitterConfig), fileName = nameof(BoxEmitterConfig))]
    public class BoxEmitterConfig : ScriptableObject
    {
        public List<Pickupable> pickupables = new();
        
        public BoxComponent boxPrefab;

        public float emissionPeriod = 3f;

        public float emissionPeriodIncreaseFactor = 5f;
    }
}