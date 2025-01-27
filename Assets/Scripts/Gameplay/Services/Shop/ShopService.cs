using Gameplay.Services.Base;
using Gameplay.Services.Shop.Config;
using Zenject;

namespace Gameplay.Services.Shop
{
    public class ShopService : GameService, IInitializable
    {
        [Inject] private ShopServiceConfig _config;
        
    }
}