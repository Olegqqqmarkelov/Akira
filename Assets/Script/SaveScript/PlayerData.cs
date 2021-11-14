using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int hp;
    public int saveSpawn;

    public PlayerData (PlayerD player)
    {
        level = player.level;
        hp = player.hp;
        saveSpawn = player.saveSpawn;
    }
}
