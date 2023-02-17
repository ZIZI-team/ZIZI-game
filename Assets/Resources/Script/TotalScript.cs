using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScript : MonoBehaviour 
{
    // public GameObject Single_1P;
    // public GameObject LocalM_2P;
    // public GameObject OnlineM;

    void Start() 
    {
        PlayerPrefs.SetInt("PlayerMode", 0);
        PlayerPrefs.SetInt("GameMode", 0);
    }

    public void PlayerMode (GameObject PlayerModeObj) 
    {
        PlayerPrefs.SetInt("PlayerMode", PlayerModeObj.transform.GetSiblingIndex() + 1);
        Debug.Log("Selected PlayerMode : " + PlayerPrefs.GetInt("PlayerMode"));
    }

    public void GameMode (GameObject GameModeObj) 
    {
        PlayerPrefs.SetInt("GameMode", GameModeObj.transform.GetSiblingIndex() + 1);
        Debug.Log("Selected GameMode : " + PlayerPrefs.GetInt("GameMode"));
    }

}
