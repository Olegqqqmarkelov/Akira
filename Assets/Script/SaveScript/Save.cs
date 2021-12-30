using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Save : MonoBehaviour
{
    [SerializeField] private GameObject Letter;
    [SerializeField] private KeyboardInput _inv;
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
            _saveSystem.SaveSpawn(IdSaveZone);
            _inv.inventory.Save();
            Letter.SetActive(false);
            isActive = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        Letter.SetActive(false);
        isActive = false;
    }
}
