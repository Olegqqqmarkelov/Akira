using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    [SerializeField] private GameObject dialogueLetter;
    public OpenDialogue openDialogue;
    private bool isActive;

    private void OnTriggerEnter(Collider other) {
        dialogueLetter.SetActive(true);
        isActive = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isActive)
        {
            dialogueLetter.SetActive(false);
            isActive = false;
            openDialogue.Open();
        }
    }
    
    private void OnTriggerExit(Collider other) {
        dialogueLetter.SetActive(false);
        openDialogue.Close();
        isActive = false;
    }
}
