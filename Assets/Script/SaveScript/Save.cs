using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Save : MonoBehaviour
{
    [SerializeField] private GameObject Letter;
    [SerializeField] public int IdSaveZone;
    [SerializeField] private KeyboardInput _keyBoard;
    [SerializeField] SaveSystem _saveSystem;

    public PlayerD _plyayerData;
    public DataBaseIdItems database;
    private bool isActive;

    private void OnTriggerEnter(Collider other) {
        Letter.SetActive(true);
        FlipCameraPlayer cameraFlip = other.GetComponent<FlipCameraPlayer>();

        if(cameraFlip != null)
        {
            if (cameraFlip.reversKey == false)
            {
                Letter.transform.eulerAngles = new Vector3(45, 0, 0);
            }
            else
            {
                Letter.transform.eulerAngles = new Vector3(45, 180, 0);
            }

            isActive = true;
        }
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

    private void SaveInventory()
    {
        var _inv = _keyBoard.inventory.Container;

        for (int i = 0; i < _inv.Count; i++)
        {
            _plyayerData.inventoryItem[i]= database.GetId[_inv[i].item];
            _plyayerData.inventoryItemAmout[i] = _inv[i].amount;
        }
    }

    private void OnTriggerExit(Collider other) {
        Letter.SetActive(false);
        isActive = false;
    }
}
