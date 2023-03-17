using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

// 기존 자신의 스킨 모양 저장

public class TotalScript : MonoBehaviour 
{

    // PlayerPrefs : PlayerMode
    // 1 : Single_1P
    // 2 : LocalM_2P
    // 3 : OnlineMulti

    // PlayerPrefs : GameMode // deleted
    // 1 : Original
    // 2 : Classic
    // 3 : Dynamic


    void Start() 
    {
        PlayerPrefs.SetInt("PlayerMode", 0);

        // PlayerPrefs.SetInt("GameMode", 0);
    }


    // Onclick Select Playermode Button
    public bool DidYouSelect_PlayerMode = false;
    // public void PlayerMode (GameObject PlayerModeObj) 
    // {
    //     PlayerPrefs.SetInt("PlayerMode", PlayerModeObj.transform.GetSiblingIndex() + 1);
    //     Debug.Log("Selected PlayerMode : " + PlayerPrefs.GetInt("PlayerMode"));


    //     // Must Select PlayerMode
    //     DidYouSelect_PlayerMode = true;
    // }

    // DidYouSelect_PlayerMode = true;

    // Onclick Select Gamemode Button
    // public bool DidYouSelect_GameMode = false;
    // public void GameMode (GameObject GameModeObj) 
    // {
    //     PlayerPrefs.SetInt("GameMode", GameModeObj.transform.GetSiblingIndex() + 1);
    //     Debug.Log("Selected GameMode : " + PlayerPrefs.GetInt("GameMode"));


    //     // Must Select GameMode
    //     DidYouSelect_GameMode = true;
    // }

    // Onclick Start Game Button
    public void StartGame()
    {
        // if (DidYouSelect_PlayerMode == true && DidYouSelect_GameMode == true)
        if (DidYouSelect_PlayerMode == true)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    
}
