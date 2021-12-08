using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class VoidRegex : MonoBehaviour
{
    public Dictionary<int, string> result = new Dictionary<int, string>(){
        {0,""},{1,""},{2,"10"},{3,"100"},{4,""},{5,""}
    };

    public Dictionary<int, string> voidRegex(string text)
    {
        result[0] = text;
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
            if (Convert.ToString(match).StartsWith("<ndn>"))
            {
                string patternNewDialogNPC = "<ndn>(.*?)</ndn>";
                Regex regexNDN = new Regex(patternNewDialogNPC);
                result[4] = "0";
                result[5] = "1";

                new_text = regexNDN.Replace(new_text, "");

                result[1] = match.Groups[1].Value;
            }

            Match match2 = Regex.Match(new_text, "<st>(.*?)</st>");
            if (Convert.ToString(match2).StartsWith("<st>"))
            {
                string patternSpeedOfText = "<st>(.*?)</st>";
                Regex regexST = new Regex(patternSpeedOfText);

                new_text = regexST.Replace(new_text, "");

                result[2] = match2.Groups[1].Value;
            }

            Match match3 = Regex.Match(new_text, "<wbw>(.*?)</wbw>");
            if (Convert.ToString(match3).StartsWith("<wbw>"))
            {
                Debug.Log(match3.Groups[1].Value);
                string patternWaitBeforWrite = "<wbw>(.*?)</wbw>";
                Regex regexWBW = new Regex(patternWaitBeforWrite);

                new_text = regexWBW.Replace(new_text, "");

                result[3] = match3.Groups[1].Value;
            }

            new_text = regexHS.Replace(new_text, targetClear);
        }

        result[0] = new_text;
        return result;
    }

}
