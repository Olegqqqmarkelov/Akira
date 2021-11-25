using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class DialogModel
{
    public int id;
    public string name;
    public string dialog;
}

public class DialogueStart : MonoBehaviour
{
    [SerializeField] private Text dialogName;
    [SerializeField] private Text dialogText;
    public List<DialogModel> dialogs = new List<DialogModel>();

    [SerializeField] private PlayerD _playerData;
    [SerializeField] private NPCData _npcData;
    [SerializeField] private KeyboardInput _playerMove;

    [SerializeField] private GameObject dialogueLetter;
    [SerializeField] private OpenDialogue openDialogue;

    private bool _isActive;
    private int idDialog = 0;
    private int lastIdDialog;
    private IEnumerator coroutine;

    private void OnTriggerEnter(Collider other) {
        if(_npcData.IdNpc == _playerData.dialogTrueIdNPC){
            dialogueLetter.SetActive(true);
            _isActive = true;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && _isActive && _npcData.IdNpc == _playerData.dialogTrueIdNPC)
        {
            dialogueLetter.SetActive(false);
            _playerMove.moveIsActive = false;

            try{
                StopWriteText();
            }catch{};

            try{
                if(dialogs.Count == 0)
                    LoadText(_playerData.dialogTrueIdNPC);
                WriteText(idDialog++);
            }catch{
                openDialogue.Close();
                dialogs.Clear();
            }   
            openDialogue.Open(); 
        }
    }

    private void LoadText(int NameFile)
    {
        TextAsset dialogsData = Resources.Load<TextAsset>("DialogData/Dialog" + NameFile.ToString());

        string[] data = dialogsData.text.Split(new char[] {'\n'});

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            DialogModel d = new DialogModel();
            int.TryParse(row[0], out d.id);
            d.name = row[1];
            d.dialog = row[2];

            dialogs.Add(d);
        }
    }

    private void WriteText(int _id)
    {
        dialogName.text = dialogs[_id].name;

        dialogText.text = "";
        coroutine = TextWriteCharByChar(VoidRegex(dialogs[_id].dialog), dialogText);
        StartCoroutine(coroutine);
    }

    private void StopWriteText()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator TextWriteCharByChar(string text, Text _lineForText) 
    {
        foreach(char _char in text) 
        {
            WriteChar(_char, _lineForText);
            yield return new WaitForSeconds(0.1f);
        }
        lastIdDialog = idDialog;
    }

    private void WriteChar(char _char, Text _lineForText) => _lineForText.text += _char.ToString();

    private string VoidRegex(string text)
    {
        string new_text;

        string patternVirgule = "<virgule/>";
        string targetClear = "";
        string targetVirgule = ",";

        Regex regex = new Regex(patternVirgule);

        new_text = regex.Replace(text, targetVirgule);

        if (new_text.StartsWith("<hs/>"))
        {
            string patternHaveScript = "<hs/>";
            Regex regexHS = new Regex(patternHaveScript);

            Match match = Regex.Match(new_text, "<ndn>(.*?)</ndn>");
            if(match.Groups[1].Value != null)
            {

                string patternNewDialogNPC = "<ndn>(.*?)</ndn>";
                Regex regexNDN = new Regex(patternNewDialogNPC);
                _isActive = false;
                _playerMove.moveIsActive = true;

                new_text = regexNDN.Replace(new_text, targetClear);

                _playerData.dialogTrueIdNPC = Convert.ToInt32(match.Groups[1].Value);
            }

            new_text = regexHS.Replace(new_text, targetClear);
        }
        return new_text;
    }

    private void OnTriggerExit(Collider other) {
        dialogueLetter.SetActive(false);
        _isActive = false;
        openDialogue.Close();
    }
}