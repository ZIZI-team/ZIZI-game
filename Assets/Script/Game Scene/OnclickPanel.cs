using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnclickPanel : MonoBehaviour
{
    GameObject GameScript;

    void Start()
    {
       GameScript = GameObject.Find("Game");    
    }

    public void OnclickP()
    {
        //GameScript.GetComponent<GameSceneSystem>().PanelOnclick();
    }
}
