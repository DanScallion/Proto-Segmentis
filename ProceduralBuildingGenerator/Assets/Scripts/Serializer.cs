using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class Serializer
{
    public static T Load<T>(string filename, bool readable = false) where T : class
    {
        if (File.Exists(filename))
        {
            try
            {
                if(readable)
                {
                    string data = File.ReadAllText(filename);
                    return JsonUtility.FromJson<T>(data);
                }
                else
                {
                    using (Stream stream = File.OpenRead(filename))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        return binaryFormatter.Deserialize(stream) as T;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        return default(T);
    }

    public static void Save<T>(string filename, T data, bool readable=false) where T : class
    {
        if(readable)
        {
            string json = JsonUtility.ToJson(data);
            //string filePath = "Assets/Runtime/save.json";
            File.WriteAllText(filename, json);
        }
        else
        {
            using (Stream stream = File.OpenWrite(filename))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
            }
        }
    }
}