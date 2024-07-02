using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전체적인 게임의 흐름을 제어하는 Class
/// </summary>
public class GameManager : Singleton<GameManager>
{
     void Start()
    {
        SetGame();
    }

    public void Update()
    {
        
    }
    /// <summary>
    /// 오목 시작 전 호출 되어지는 함수
    /// </summary>
    public void SetGame()
    {
        NetworkManager.Instance.SendSetTile();
        StartCoroutine(TileManager.Instance.GetTile());
        GameStart();
    }
    /// <summary>
    /// SetGame()이 끝나면 호출 되어지는 함수
    /// </summary>
    public void GameStart()
    {
        //이 값이 true가 되어야만 돌을 놓을 수 있게 됨.
        DataManager.Instance.gamedata.isGameStart = true;
    }
    /// <summary>
    /// 게임 종료 조건 만족 시 호출 되어지는 함수
    /// </summary>
    public void GameEnd()
    {
        UIManager.Instance.InstantiateWinnerPanel();
        NetworkManager.Instance.SendRPCEndGame();
    }

    public void RestartGame()
    {

    }

    
    

}
