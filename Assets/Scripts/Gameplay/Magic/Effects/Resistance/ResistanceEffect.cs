using System;
using System.Collections;
using Gameplay.Core.Container;
using Gameplay.Magic.Effects.Base;
using UnityEngine;

namespace Gameplay.Magic.Effects.Resistance
{
    public class ResistanceEffect : Effect
    {
        public override Coroutine CurrentCoroutine { get; set; }

        protected override void Execute(ComponentContainer component)
        {
        }

        public override Action<Effect> EffectElapsed { get; set; }
    }
}