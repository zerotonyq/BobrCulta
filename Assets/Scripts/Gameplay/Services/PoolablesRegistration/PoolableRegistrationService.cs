using Gameplay.Services.Base;
using Gameplay.Services.PoolablesRegistration.Config;
using Utils.Pooling;
using Zenject;

namespace Gameplay.Services.Pool
{
    public class PoolableRegistrationService : GameService, IInitializable
    {
        [Inject] private PoolableRegistrationServiceConfig _config;

        public override void Initialize()
        {
            foreach (var prefab in _config.prefabs)
            {
                PoolManager.RegisterPrefab(prefab.GetType(), prefab.gameObject);
            }
            base.Initialize();
        }
    }
}