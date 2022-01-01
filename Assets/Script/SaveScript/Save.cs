using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Save : MonoBehaviour
{
    [SerializeField] private GameObject Letter;
    [SerializeField] private PlayerD data;
    [SerializeField] private KeyboardInput _keyBoard;
    [SerializeField] public int IdSaveZone;
    [SerializeField] SaveSystem _saveSystem;
    private bool isActive;

    private void OnTriggerEnter(Collider other) {
        Letter.SetActive(true);
        isActive = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isActive)
        {
            SaveInventory();
            _saveSystem.SaveSpawn(IdSaveZone);
            Letter.SetActive(false);
            isActive = false;
        }
    }

    void SaveInventory()
    {
        for (int i = 0; i < _keyBoard.inventory.Container.Count; i++)
        {
            data.inv.Add(i, _keyBoard.inventory.Container[i]);
        }
    }

    private void OnTriggerExit(Collider other) {
        Letter.SetActive(false);
        isActive = false;
    }
}
