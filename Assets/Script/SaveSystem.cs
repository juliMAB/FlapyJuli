using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveSystem
{
    public static void SaveData ()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.datajuli";
        FileStream stream = new FileStream (path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/data.datajuli";
        if (!File.Exists(path))
            return null;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream (path, FileMode.Open);

        PlayerData data = formatter.Deserialize(stream) as PlayerData;
        stream.Close();

        return data;
    }
}
