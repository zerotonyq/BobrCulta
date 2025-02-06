using Gameplay.Core.Base;
using Gameplay.Magic.Barrel;
using UnityEngine;

namespace Gameplay.Magic.Abilities.WoodShield
{
    [RequireComponent(typeof(MagicAbilityComponent))]
    public class AutomationSingleAbilityBinder : Binder
    {
        [SerializeField] private MagicPickupableBarrelComponent.MagicTypeArgs args;
        public override void Bind()
        {
            GetComponent<MagicAbilityComponent>().EmitMagicAbility(args);
        }
    }
}