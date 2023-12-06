using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LeaderboardDownload : MonoBehaviour
{
    public TextMeshProUGUI placements;
    public TextMeshProUGUI names;
    public TextMeshProUGUI scores;
    public TextMeshProUGUI stars;

    //http://dreamlo.com/lb/bY-Fks3l_0GxTE0BTHT2gg0sj8g8IcEUiC7Z1C1KfV8g
    const string privateCode = "bY-Fks3l_0GxTE0BTHT2gg0sj8g8IcEUiC7Z1C1KfV8g";
    const string publicCode = "6570a49b8f40bb1054d89966";
    const string webURL = "http://dreamlo.com/lb/";

    [System.Obsolete]
    void Start()
    {
        StartCoroutine(DownloadHighscores());
    }

    [System.Obsolete]
    IEnumerator DownloadHighscores()
    {
        WWW www = new WWW(webURL + privateCode + "/pipe/");
        yield return www;

        if(string.IsNullOrEmpty(www.error))
            FormatText(www.text);
        else
            Debug.Log("Download Failed: " + www.error);
    }

    void FormatText(string text)
    {
        string[] textLines = text.Split(new char[]{'\n'}, System.StringSplitOptions.RemoveEmptyEntries);

        for(int i = 0; i < textLines.Length; i++)
        {
            string[] entryInfo;

            entryInfo = textLines[i].Split(new char[] {'|'});
            
            placements.text = placements.text + "\n" + (i+1) + ".";
            names.text = names.text.Replace("+", " ") + "\n" + entryInfo[0];
            scores.text = scores.text + "\n" + entryInfo[1];
            stars.text = stars.text + "\n" + entryInfo[2];
        }
    }
}
