using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIZI : MonoBehaviour
{
    GameObject Game;

    void Start()
    {
        Game = GameObject.Find("Game");   
    }

    public void OnclickZIZI_Func()
    {
        Debug.Log(gameObject.name);

        Game.GetComponent<GameSceneSystem>().OnclickZIZI(gameObject);
    }
}
