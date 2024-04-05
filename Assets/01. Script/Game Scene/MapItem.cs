using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    GameObject Game;

    void Start()
    {
        Game = GameObject.Find("Game");   
    }

    public void OnclickMapItem_Func()
    {
        Game.GetComponent<GameSceneSystem>().OnclickMapItem(gameObject);
    }
}
