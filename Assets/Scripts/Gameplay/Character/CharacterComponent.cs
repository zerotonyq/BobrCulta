using System.Threading.Tasks;
using Gameplay.Player.Base;
using Gameplay.Player.Config;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Initialize;

namespace Gameplay.Character
{
    public class CharacterComponent : MonoBehaviour, IInitializableConfig, ICharacter
    {
        private DisposableBag _disposableBag;
        
        public async Task Initialize(ScriptableObject config)
        {
            var concreteConfig = (CharacterConfig)config;
            
            foreach (var c in concreteConfig.configs)
            {
                if (!TryGetComponent(c.InitializableType, out var component))
                    return;
                
                await (component.GetComponent(c.InitializableType) as IInitializableConfig).Initialize(c);
            }
        }
        
        public void Destroy() => Addressables.ReleaseInstance(gameObject);
        
        private void OnDestroy() => _disposableBag.Dispose();

        public void Dispose() => _disposableBag.Dispose();

        public Vector3 GetPosition() => transform.position;
        public Transform Transform  => transform;
    }
}