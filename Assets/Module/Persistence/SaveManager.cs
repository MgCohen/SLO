using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager
{
    private static string path = Application.persistentDataPath;

    public void Save<T>(T content)
    {
        string json = JsonUtility.ToJson(content);
        string fullPath = path + "/" + content.GetType().Name;
        File.WriteAllText(fullPath, json);
    }   

    public void Load(object instance)
    {
        string fullPath = path + "/" + instance.GetType().Name;
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            JsonUtility.FromJsonOverwrite(json, instance);
        }
    }
}
