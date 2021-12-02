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
    private float speedOfText = 0.075f;
    private float waitBeforWrite = 1f;
    private IEnumerator coroutine;

    private void Awake()
    {
        try{
            idDialog = _playerData.dialogId[_npcData.IdNpc];
        }catch{_playerData.dialogId.Add(_npcData.IdNpc,0);};
    }

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
                StopCoroutine(coroutine);
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
        _playerData.dialogId[_npcData.IdNpc] = _id;

        dialogName.text = dialogs[_id].name;

        dialogText.text = "";
        speedOfText = 0.075f;
        waitBeforWrite = 1f;

        coroutine = TextWriteCharByChar(VoidRegex(dialogs[_id].dialog), dialogText);
        StartCoroutine(coroutine);
    }

    private IEnumerator TextWriteCharByChar(string text, Text _lineForText) 
    {
        foreach(char _char in text) 
        {
            _lineForText.text += _char.ToString();
            yield return new WaitForSeconds(speedOfText);
        }

        yield return new WaitForSeconds(waitBeforWrite);
    }

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
            if(Convert.ToString(match).StartsWith("<ndn>"))
            {
                string patternNewDialogNPC = "<ndn>(.*?)</ndn>";
                Regex regexNDN = new Regex(patternNewDialogNPC);
                _isActive = false;
                _playerMove.moveIsActive = true;

                new_text = regexNDN.Replace(new_text, "");

                _playerData.dialogTrueIdNPC = Convert.ToInt32(match.Groups[1].Value);
            }

            Match match2 = Regex.Match(new_text, "<st>(.*?)</st>");
            if(Convert.ToString(match2).StartsWith("<st>"))
            {
                string patternSpeedOfText = "<st>(.*?)</st>";
                Regex regexST = new Regex(patternSpeedOfText);

                new_text = regexST.Replace(new_text, "");

                speedOfText = float.Parse(match2.Groups[1].Value) / 100;
            }

            Match match3 = Regex.Match(new_text, "<wbw>(.*?)</wbw>");
            if(Convert.ToString(match3).StartsWith("<wbw>"))
            {
                Debug.Log(match3.Groups[1].Value);
                string patternWaitBeforWrite = "<wbw>(.*?)</wbw>";
                Regex regexWBW = new Regex(patternWaitBeforWrite);

                new_text = regexWBW.Replace(new_text, "");

                waitBeforWrite = float.Parse(match3.Groups[1].Value) / 100;
            }
            
            new_text = regexHS.Replace(new_text, targetClear);
        }
        return new_text;
    }

    private void OnTriggerExit(Collider other) {
        try{
            StopCoroutine(coroutine);
        }catch{};

        dialogueLetter.SetActive(false);
        _isActive = false;
        openDialogue.Close();
    }
}