using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public PlayerD player;

    public void SaveSpawn(int IdSaveZone)
    {
        string path = Application.persistentDataPath + "/player.fun";

        PlayerData pd = new PlayerData(player);
        pd.saveSpawn = IdSaveZone;

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, pd);
        stream.Close();
    }

    public PlayerData LoadSaveData()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if (File.Exists(path))
        {
            try {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
            } catch(System.Exception)
            {
                return null;
            }
        }
        else
        {
            Debug.LogError("Save file not found in");
            return null;
        }
    }

}
