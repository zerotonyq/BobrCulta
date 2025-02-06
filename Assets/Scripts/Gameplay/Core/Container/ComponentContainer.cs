using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Core.Base;
using Gameplay.Core.Container.Base;
using UnityEngine;
using Utils.Activate;
using Utils.Reset;

namespace Gameplay.Core.Container
{
    public class ComponentContainer : MonoComponentContainer, IActivateable, IResetable
    {
        public override List<MonoComponent> Components { get; } = new();
        
        public override Task Initialize()
        {
            foreach (var monoComponent in GetComponentsInChildren<MonoComponent>())
            {
                monoComponent.Initialize();

                Components.Add(monoComponent);
            }

            foreach (var inputBinder in GetComponents<Binder>())
            {
                inputBinder.Bind();
            }

            return Task.CompletedTask;
        }

        public Action<GameObject> Deactivated { get; set; }

        public void Activate(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            Deactivated?.Invoke(gameObject);
        }

        public void Reset()
        {
            foreach (var component in Components)
            {
                component.Reset();
            }
        }
    }
}