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
        TileManager.Instance.InitTile();
    }
    public void GameStart()
    {
        
    }

    public void GameEnd()
    {
        Debug.Log("����ϼ�");
    }

    public void RestartGame()
    {

    }

    
    

}
