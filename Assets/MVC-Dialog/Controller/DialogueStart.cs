using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogModel
{
    public int id;
    public string name;
    public string dialog;
}

public class DialogueStart : MonoBehaviour
{
    public int asd = 1;

    [SerializeField] private Text dialogName;
    [SerializeField] private Text dialogText;
    public List<DialogModel> dialogs = new List<DialogModel>();

    [SerializeField] private PlayerD _playerData;
    [SerializeField] private NPCData _npcData;

    [SerializeField] private GameObject dialogueLetter;
    [SerializeField] private OpenDialogue openDialogue;
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

            if(dialogs.Count == 0){
                LoadText(_playerData.chapter);
                WriteText(asd);
            }else{WriteText(asd);}
            WriteText(asd);
            asd++;

            openDialogue.Open();
        }
    }

    public void LoadText(int NameFile)
    {
        TextAsset dialogsData = Resources.Load<TextAsset>("DialogData/Charter" + NameFile.ToString());

        string[] data = dialogsData.text.Split(new char[] {'\n'});

        for (int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            DialogModel d = new DialogModel();
            int.TryParse(row[0], out d.id);
            d.name = row[1];
            d.dialog = row[2];

            dialogs.Add(d);
        }
    }

    public void WriteText(int asd)
    {
        dialogName.text = dialogs[asd].name;
        dialogText.text = dialogs[asd].dialog;
    }

    private void OnTriggerExit(Collider other) {
        dialogueLetter.SetActive(false);
        openDialogue.Close();
        isActive = false;
    }
}