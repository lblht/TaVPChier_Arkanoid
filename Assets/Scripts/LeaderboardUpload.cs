using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardUpload : MonoBehaviour
{
    //http://dreamlo.com/lb/bY-Fks3l_0GxTE0BTHT2gg0sj8g8IcEUiC7Z1C1KfV8g
    const string privateCode = "bY-Fks3l_0GxTE0BTHT2gg0sj8g8IcEUiC7Z1C1KfV8g";
    const string publicCode = "6570a49b8f40bb1054d89966";
    const string webURL = "http://dreamlo.com/lb/";

    [System.Obsolete]
    public void UploadLeaderboard()
    {
        string playerName = GameManager.Instance.GetPlayerName();
        int score = GameManager.Instance.GetTotalScore();
        int stars = GameManager.Instance.GetTotalStars();

        if(playerName != "UnknownPlayer" && playerName != "" && score > 0) 
        {
            WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(playerName) + "/" + score + "/" + stars);

            if(string.IsNullOrEmpty(www.error))
                Debug.Log("New Highscore Uploaded");
            else
                Debug.Log("Upload Failed: " + www.error);
        }
    }
}
