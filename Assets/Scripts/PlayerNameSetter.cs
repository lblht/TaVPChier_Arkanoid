using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameSetter : MonoBehaviour
{

    void Start()
    {
        GetComponent<TMP_InputField>().text = GameManager.Instance.GetPlayerName();
    }

    public void UpdatePlayerName(string playerName)
    {
        GameManager.Instance.SetPlayerName(playerName);
    }
}
