using System;
using System.Collections.Generic;
using Modifiers.Base;

namespace Modifiers
{
    public static class ModifierProvider
    {
        public static Dictionary<Type, List<Modifier>> Modifiers = new();

        public static void AddModifier(Modifier m)
        {
            
            if (Modifiers.ContainsKey(m.GetType()))
                Modifiers[m.GetType()].Add(m);
            else
                Modifiers.Add(m.GetType(), new List<Modifier> { m });
        }
    }
}