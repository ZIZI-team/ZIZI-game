using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : Singleton<GameSystem>
{
    private void Update()
    {
        if(DataManager.Instance.gamedata.isGameStart == true)
        {
            if (IsMyturn()) 
            { 
                TileManager.Instance.OnClickPosition();
            }
        }
    }
    bool IsMyturn()
    {
        //턴의 상태
        string turnData = DataManager.Instance.gamedata.turnData;
        //나 player number P1 or P2
        string mystate = DataManager.Instance.gamedata.myP;
        Debug.Log(turnData + "  "  + mystate);
        return turnData == mystate;
    }

    public void changeTurn()
    {
        string tmp = DataManager.Instance.gamedata.turnData == "P1" ? "P2" : "P1";
        DataManager.Instance.gamedata.turnData = tmp;
        DataManager.Instance.InitTimer();
    }

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
