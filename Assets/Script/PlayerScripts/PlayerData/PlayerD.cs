using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerD : MonoBehaviour
{
    [SerializeField] SaveSystem _saveSystem;
    [SerializeField] KeyboardInput _keyBoard;

    public int level = 0;
    public Dictionary<int, int> dialogId = new Dictionary<int,int>(){
        {0,0},
    };
    public Dictionary<int, int> autoDialogId = new Dictionary<int,int>(){
        {0,0},
    };
    public Dictionary<int, int> inventoryItem = new Dictionary<int, int>()
    {};
    public Dictionary<int, int> inventoryItemAmout = new Dictionary<int, int>()
    {};
    public int dialogTrueIdNPC = 0;
    public int autoDialogTrueIdTriger = 0;
    public int respect = 100;
    public int hp = 100;
    public int saveSpawn = 0;


    void Awake()
    {
        PlayerData data = _saveSystem.LoadSaveData();

        level = data.level;
        dialogId = data.dialogId;
        autoDialogId = data.autoDialogId;
        inventoryItem = data.inventoryItem;
        inventoryItemAmout = data.inventoryItemAmout;
        dialogTrueIdNPC = data.dialogTrueIdNPC;
        autoDialogTrueIdTriger = data.autoDialogTrueIdTriger;
        respect = data.respect;
        hp = data.hp;
        saveSpawn = data.saveSpawn;
    }
    void Start()
    {
        //Spawn player in save point
        GameObject pointSpawn = GameObject.Find("SpawnPoint" + saveSpawn.ToString());
        transform.position = pointSpawn.transform.position;

        //Load saved inventory
        _keyBoard.inventory.database.OnAfterDeserialize();
        for (int i = 0; i < inventoryItem.Count; i++)
        {
            _keyBoard.inventory.AddItem(
                _keyBoard.inventory.database.GetItem[inventoryItem[i]],
                inventoryItemAmout[i]
                );
        }
    }
}
