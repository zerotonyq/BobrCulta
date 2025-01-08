using System;
using System.IO;
using UnityEngine;

namespace Utils.SaveData
{
    public static class JsonDataProvider
    {
        private static readonly string SaveBasePath = Application.dataPath + "/PersistConfigs";

        public static void Save(object obj, string relativePath, string name)
        {
            try
            {
                if (!Directory.Exists(SaveBasePath))
                    Directory.CreateDirectory(SaveBasePath);

                if (string.IsNullOrEmpty(name))
                {
                    Debug.LogError("empty file name");
                    return;
                }

                if (!Directory.Exists(SaveBasePath + "/" + relativePath))
                    Directory.CreateDirectory(SaveBasePath + "/" + relativePath);


                File.WriteAllText(SaveBasePath + "/" + relativePath + "/" + name + ".dat", JsonUtility.ToJson(obj, true));
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        public static void Load(object obj, string relativePath, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("empty file name");
                return;
            }

            if (!Directory.Exists(SaveBasePath + "/" + relativePath))
            {
                Debug.LogError("directory is not exist");
                return;
            }

            var jsonString = File.ReadAllText(SaveBasePath + "/" + relativePath + "/" + name + ".dat");
            
            JsonUtility.FromJsonOverwrite(jsonString, obj);
        }
    }
}