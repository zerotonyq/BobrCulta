using Input;
using UnityEngine.AddressableAssets;
using Utils.Disposing;
using Zenject;

namespace DI
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Addressables.InitializeAsync();
        }
    }
}