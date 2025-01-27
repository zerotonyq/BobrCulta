using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Services.UI.Gameplay.Magic.Elements
{
    public class ProjectileUIElement : MonoBehaviour
    {
        [field: SerializeField] public Image Image { get; private set; }
        public Type Type { get; private set; }

        public void Set(Sprite sprite, Type type)
        {
            Type = type;
            Image.sprite = sprite;
        }

        public void Clear() => Set(null, null);
    }
}