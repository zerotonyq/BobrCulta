using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Utils.Initialize
{
    public interface IInitializableConfig : IDisposable
    {
        Task Initialize(ScriptableObject config);
    }
}