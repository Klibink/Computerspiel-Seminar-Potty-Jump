using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(GameManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/player.dat";
        string path = Path.Combine(Application.persistentDataPath, "player.dat");
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        //string path = Application.persistentDataPath + "/player.dat";
        string path = Path.Combine(Application.persistentDataPath, "player.dat");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            try
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                return data;
            }
            finally
            {
                stream.Close();
            }
            

            
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
