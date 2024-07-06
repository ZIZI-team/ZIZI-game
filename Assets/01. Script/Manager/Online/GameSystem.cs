using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오목 게임의 전반적인 system에 대한 클래스
/// </summary>
public class GameSystem : Singleton<GameSystem>
{
    private void Update()
    {
        //게임이 시작 시
        if(DataManager.Instance.gamedata.isGameStart == true)
        {
            //나의 턴이면
            if (IsMyturn()) 
            { 
                //터치한 위치 좌표를 반환하는 함수 호출
                TileManager.Instance.OnClickPosition();
            }
        }
    }

    /// <summary>
    /// 현재 자신의 차례인지 확인하는 함수
    /// </summary>
    /// <returns>
    /// 현재 자신의 차례라면 true, 아니면 false를 반환합니다.
    /// </returns>
    bool IsMyturn()
    {
        // 게임 데이터에서 현재 턴의 상태를 가져옵니다.
        // 예: "P1" 또는 "P2"
        string turnData = DataManager.Instance.gamedata.turnData;

        // 게임 데이터에서 내 플레이어 상태를 가져옵니다.
        // 예: "P1" 또는 "P2"
        string mystate = DataManager.Instance.gamedata.myP;

        // 현재 턴의 상태와 내 플레이어 상태가 같은지 확인하여
        // 내 차례인지 여부를 반환합니다.
        return turnData == mystate;
    }

    /// <summary>
    /// 턴을 변경하는 함수
    /// </summary>
    public void changeTurn()
    {
        // 게임 데이터에서 현재 턴을 가져와 그와 반대 되는 턴을 저장합니다.
        string change = DataManager.Instance.gamedata.turnData == "P1" ? "P2" : "P1";
        // 게임 데이터에 바뀐 턴을 저장합니다.
        DataManager.Instance.gamedata.turnData = change;
        //타이머를 초기화 시킵니다.
        DataManager.Instance.InitTimer();
    }

    /// <summary>
    /// 현재 플레이어의 승리 조건을 확인하는 코루틴 함수
    /// </summary>
    /// <param name="player">현재 플레이어의 정보(P1 또는 P2)</param>
    /// <param name="x">현재 플레이어가 놓은 돌의 x좌표</param>
    /// <param name="y">현재 플레이어가 놓은 돌의 y좌표</param>
    /// <returns>코루틴 실행 대기</returns>
    public IEnumerator CheckWinCondition(string player, int x, int y)
    {
        yield return null;
        // 오목판의 크기
        int boardSize = 11;

        Debug.Log("CheckWinCondition 함수 실행");
        // 각 방향별로 연속된 돌의 개수를 세는 변수들
        int countVertical = 1;
        int countHorizontal = 1;
        int countDiagonal1 = 1;
        int countDiagonal2 = 1;

        string[,] board = DataManager.Instance.tiledata.stoneStatus;
        // 수직 방향 확인
        for (int i = 1; i < 5; i++)
        {
            if (y + i < boardSize && board[x, y + i] == player)
                countVertical++;
            else
                break;
        }

        for (int i = 1; i < 5; i++)
        {
            if (y - i >= 0 && board[x, y - i] == player)
                countVertical++;
            else
                break;
        }

        // 수평 방향 확인
        for (int i = 1; i < 5; i++)
        {
            if (x + i < boardSize && board[x + i, y] == player)
                countHorizontal++;
            else
                break;
        }

        for (int i = 1; i < 5; i++)
        {
            if (x - i >= 0 && board[x - i, y] == player)
                countHorizontal++;
            else
                break;
        }

        // 대각선 방향 확인 (우측 아래로)
        for (int i = 1; i < 5; i++)
        {
            if (x + i < boardSize && y + i < boardSize && board[x + i, y + i] == player)
                countDiagonal1++;
            else
                break;
        }

        for (int i = 1; i < 5; i++)
        {
            if (x - i >= 0 && y - i >= 0 && board[x - i, y - i] == player)
                countDiagonal1++;
            else
                break;
        }

        // 대각선 방향 확인 (우측 위로)
        for (int i = 1; i < 5; i++)
        {
            if (x + i < boardSize && y - i >= 0 && board[x + i, y - i] == player)
                countDiagonal2++;
            else
                break;
        }

        for (int i = 1; i < 5; i++)
        {
            if (x - i >= 0 && y + i < boardSize && board[x - i, y + i] == player)
                countDiagonal2++;
            else
                break;
        }

        // 어느 한 방향으로 5개 이상의 돌이 연속되어 있으면 승리
        if (countVertical >= 5 || countHorizontal >= 5 || countDiagonal1 >= 5 || countDiagonal2 >= 5)
        {
            GameManager.Instance.GameEnd();
        }


    }
}
