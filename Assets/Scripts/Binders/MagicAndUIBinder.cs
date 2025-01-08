using Gameplay.Core.Pickup;
using Gameplay.Magic;
using UI.Magic;

namespace Binders
{
    public class MagicAndUIBinder 
    {
        public void Bind(MagicComponent magicComponent, MagicProjectilesUIManager magicProjectilesUIManager)
        {
            magicComponent.MagicPickupableProvided += magicProjectilesUIManager.OnMagicProjectileProvided;

            magicProjectilesUIManager.MagicTypeProvided += magicComponent.FireProjectile;
            magicProjectilesUIManager.MagicTypeRemoved += magicComponent.RemoveProjectile;
        }
    }
}