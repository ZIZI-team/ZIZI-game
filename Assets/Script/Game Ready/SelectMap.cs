using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMap : MonoBehaviour
{
    GameObject GameReady;

    void Start()
    {
        GameReady = GameObject.Find("ReadyGame");   
    }

    public void Finish_SelectMap_Func()
    {
        GameReady.GetComponent<GameReadyHub>().Finish_SelectMap(gameObject);
    }
}
