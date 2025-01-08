﻿using UnityEngine.AddressableAssets;
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