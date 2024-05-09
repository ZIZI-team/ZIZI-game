using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    void Start()
    {
        SetGame();
    }

    public void Update()
    {
        
    }
    public void SetGame()
    {
        TileManager.Instance.InitTile();
        DataManager.Instance.gamedata.isGameStart = true;
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
