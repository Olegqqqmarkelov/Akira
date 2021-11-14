using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public PlayerD player;
    
    public void SaveSpawn(int IdSaveZone)
    {
        Debug.Log(IdSaveZone);
        PlayerData pd = new PlayerData(player);
        pd.saveSpawn = IdSaveZone;

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, pd);
        stream.Close();
    }

    // public PlayerData LoadSaveData()
    // {
    // }
}
