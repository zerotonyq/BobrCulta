using UnityEngine;

namespace Utils.SaveData.Base
{
    public interface ISaveable
    {
        ScriptableObject Save(string parent);
    }
}