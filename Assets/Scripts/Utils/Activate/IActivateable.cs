using UnityEngine;

namespace Utils.Activate
{
    public interface IActivateable
    {
        void Activate(Vector3 position);
        void Deactivate();
    }
}