using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���� �ڽ��� ��Ų ��� ����

public class TotalScript : MonoBehaviour 
{

    // PlayerPrefs : PlayerMode
    // 1 : Single_1P
    // 2 : LocalM_2P
    // 3 : OnlineMulti

    // PlayerPrefs : GameMode
    // 1 : Original
    // 2 : Classic
    // 3 : Dynamic


    void Start() 
    {
        PlayerPrefs.SetInt("PlayerMode", 0);
        PlayerPrefs.SetInt("GameMode", 0);
    }

    // Onclick Select Playermode Button
    public void PlayerMode (GameObject PlayerModeObj) 
    {
        PlayerPrefs.SetInt("PlayerMode", PlayerModeObj.transform.GetSiblingIndex() + 1);
        Debug.Log("Selected PlayerMode : " + PlayerPrefs.GetInt("PlayerMode"));
    }

    // Onclick Select Gamemode Button
    public void GameMode (GameObject GameModeObj) 
    {
        PlayerPrefs.SetInt("GameMode", GameModeObj.transform.GetSiblingIndex() + 1);
        Debug.Log("Selected GameMode : " + PlayerPrefs.GetInt("GameMode"));
    }

    // Onclick Start Game Button
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

}
