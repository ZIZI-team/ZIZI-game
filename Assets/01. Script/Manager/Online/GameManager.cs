using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public void Update()
    {
        
    }
    public void SetGame()
    {
        DataManager.Instance.gamedata.isGameStart = true;
        NetworkManager.Instance.SendSetTile();
        StartCoroutine(TileManager.Instance.GetTile());
        
    }
    public void GameStart()
    {
        
    }

    public void GameEnd()
    {
        UIManager.Instance.InstantiateWinnerPanel();
        NetworkManager.Instance.SendRPCEndGame();
    }

    public void RestartGame()
    {

    }

    
    

}
