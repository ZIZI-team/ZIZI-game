using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    GameObject Game;

    void Start()
    {
       Game = GameObject.Find("Game");    
    }

    public void OnclickRock_Func()
    {
        Game.GetComponent<GameSceneSystem>().OnclickRock(gameObject);
    }
}
