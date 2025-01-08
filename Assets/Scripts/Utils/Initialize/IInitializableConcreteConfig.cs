using System.Threading.Tasks;
using UnityEngine;

namespace Utils.Initialize
{
    public interface IInitializableConcreteConfig<in T> where T : ScriptableObject
    {
        Task Initialize(T config);
    }
}