using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DialogModel
{
    public int id;
    public string name;
    public string dialog;
}

public class TextController : MonoBehaviour
{
    // public CharterDialogModel DialogModel;
    List<DialogModel> dialogs = new List<DialogModel>();

    void Start()
    {
        RoadText("123");
        WriteText();
    }

    void RoadText(string NameFile)
    {
        TextAsset dialogsData = Resources.Load<TextAsset>("test");

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

    void WriteText()
    {
        // foreach(DialogModel d in dialogs)
        // {
        //     Debug.Log(d.id + " " + d.name + " " + d.dialog);
        // }
        // Debug.Log("sad");
        Debug.Log(dialogs[1].name);
    }


}
