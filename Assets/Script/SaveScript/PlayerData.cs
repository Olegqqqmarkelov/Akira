using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int level;
    public Dictionary<int,int> dialogId;
    public Dictionary<int,int> autoDialogId;
    public int dialogTrueIdNPC;
    public int autoDialogTrueIdTriger;
    public int hp;
    public int saveSpawn;

    public PlayerData (PlayerD player)
    {
        level = player.level;
        dialogId = player.dialogId;
        autoDialogId = player.autoDialogId;
        dialogTrueIdNPC = player.dialogTrueIdNPC;
        autoDialogTrueIdTriger = player.autoDialogTrueIdTriger;
        hp = player.hp;
        saveSpawn = player.saveSpawn;
    }
    
}
