using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerD : MonoBehaviour
{
    [SerializeField] SaveSystem _saveSystem;

    public int level = 0;
    public int chapter = 0;
    public int dialogId = 0;
    public int hp = 100;
    public int saveSpawn = 0;  


    void Awake()
    {
        PlayerData data = _saveSystem.LoadSaveData();

        level = data.level;
        hp = data.hp;
        saveSpawn = data.saveSpawn;
    }
    void Start()
    {
        //Spawn player in save point
        GameObject pointSpawn = GameObject.Find("SpawnPoint" + saveSpawn.ToString());
        
        transform.position = pointSpawn.transform.position;
    }
}
