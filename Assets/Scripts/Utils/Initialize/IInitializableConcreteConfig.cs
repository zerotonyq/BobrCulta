using System.Threading.Tasks;
using Zenject;

namespace Utils.Initialize
{
    public interface IInitializableConcreteConfig<in T> where T : Config
    {
        Task Initialize(T config);
    }
}