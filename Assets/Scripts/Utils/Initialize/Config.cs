using System;
using UnityEngine;

namespace Utils.Initialize
{
    public abstract class Config : ScriptableObject
    {
        public const string ConfigMenuName = "CreateConfig/";
        
        public abstract Type InitializableType { get; }
    }
}