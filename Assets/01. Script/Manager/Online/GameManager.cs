using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Intance
    {
        get
        {
            if(instance == null) { return null; }
            return instance;
        }
    }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Update()
    {
        
    }
    public void SetGame()
    {
        TileManager.Instance.InitTile();
    }
    public void GameStart()
    {
        
    }

    public void GameEnd()
    {
        Debug.Log("오목완성");
    }

    public void RestartGame()
    {

    }

    

}
