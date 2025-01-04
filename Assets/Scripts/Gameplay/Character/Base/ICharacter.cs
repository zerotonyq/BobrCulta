using UnityEngine;

namespace Gameplay.Player.Base
{
    public interface ICharacter
    {
        Vector3 GetPosition();
        Transform Transform { get; }

        void Destroy();
    }
}