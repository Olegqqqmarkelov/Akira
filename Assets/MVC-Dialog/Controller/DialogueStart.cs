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
    private int idDialgo = 0;

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
        if(Input.GetKeyDown(KeyCode.E))
        {
            dialogueLetter.SetActive(false);
            

            if(dialogs.Count == 0){
                LoadText(_playerData.chapter);
            }else{WriteText(idDialgo++);}
            WriteText(idDialgo++);

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
        dialogText.text = "";
        StartCoroutine(TextCoroutine(dialogs[asd].dialog));
    }

    public IEnumerator TextCoroutine(string text) 
    {
        foreach(char c in text) 
        {
            dialogText.text += c.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerExit(Collider other) {
        dialogueLetter.SetActive(false);
        openDialogue.Close();
        isActive = false;
    }
}