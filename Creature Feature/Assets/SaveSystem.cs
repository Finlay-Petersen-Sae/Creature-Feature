using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/WorldState.cat";
        FileStream stream = new FileStream(path, FileMode.Create);

        Serialization serialization = Serialization.GetInstance();

        formatter.Serialize(stream, serialization);
        stream.Close();
    }

    public static Serialization LoadData()
    {
        string path = Application.persistentDataPath + "/WorldState.cat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           Serialization data = formatter.Deserialize(stream) as Serialization;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Shits Fucked" + path);

            return null;
            
        }
    }
}
