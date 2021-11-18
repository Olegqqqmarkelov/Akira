using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int chapter = 0;
    public int dialogId = 0;
    public int dialogTrueIdNPC;
    public int hp;
    public int saveSpawn;

    public PlayerData (PlayerD player)
    {
        level = player.level;
        chapter = player.chapter;
        dialogId = player.dialogId;
        dialogTrueIdNPC = player.dialogTrueIdNPC;
        hp = player.hp;
        saveSpawn = player.saveSpawn;
    }
    
}
