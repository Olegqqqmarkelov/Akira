using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadText : MonoBehaviour
{
    List<Quest> quests = new List<Quest>();

    void Start()
    {
        TextAsset questData = Resources.Load<TextAsset>("test");

        string[] data = questData.text.Split(new char[] {'\n'});

        for (int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Quest q = new Quest();
            int.TryParse(row[0], out q.id);
            q.name = row[1];
            q.category = row[2];
            q.desc = row[3];

            quests.Add(q);
        }

        foreach (Quest q in quests)
        {
            Debug.Log(q.id + " " + q.name);
        }
    }
}
