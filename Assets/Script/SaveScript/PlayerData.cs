using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int level;
    public Dictionary<int,int> dialogId;
    public Dictionary<int,int> autoDialogId;
    public Dictionary<int,int> inventoryItem;
    public Dictionary<int,int> inventoryItemAmout;
    public int dialogTrueIdNPC;
    public int autoDialogTrueIdTriger;
    public int respect;
    public int hp;
    public int saveSpawn;

    public PlayerData (PlayerD player)
    {
        level = player.level;
        dialogId = player.dialogId;
        autoDialogId = player.autoDialogId;
        inventoryItem = player.inventoryItem;
        inventoryItemAmout = player.inventoryItemAmout;
        dialogTrueIdNPC = player.dialogTrueIdNPC;
        autoDialogTrueIdTriger = player.autoDialogTrueIdTriger;
        respect = player.respect;
        hp = player.hp;
        saveSpawn = player.saveSpawn;
    }
    
}
