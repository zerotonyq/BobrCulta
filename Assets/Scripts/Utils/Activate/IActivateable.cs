using System;
using UnityEngine;

namespace Utils.Activate
{
    public interface IActivateable
    {
        Action<GameObject> Deactivated { get; set; }
        void Activate(Vector3 position);
        void Deactivate();
    }
}