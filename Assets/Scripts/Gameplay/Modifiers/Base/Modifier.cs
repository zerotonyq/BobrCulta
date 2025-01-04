using System.Threading.Tasks;
using UnityEngine;
using Utils.Initialize;

namespace Modifiers.Base
{
    
    public abstract class Modifier : IInitializableConcreteConfig<ModifierConfig>
    {
        public virtual Task Initialize(ModifierConfig config)
        {
            ModifierProvider.AddModifier(this);
            return Task.CompletedTask;
        }

    }
}