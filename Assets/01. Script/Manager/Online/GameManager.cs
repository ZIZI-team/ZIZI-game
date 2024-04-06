using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager intances;

    public static GameManager Intances
    {
        get
        {
            if(intances == null) { return null; }
            return intances;
        }
    }

    public void Awake()
    {
        if(intances == null)
        {
            intances = this;
        }
    }

    public void Update()
    {
        
    }
    public void SetGame()
    {

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

    public void CheckWinCondition(string player, int x, int y)
    {
        // 오목판의 크기
        int boardSize = 11;

        // 각 방향별로 연속된 돌의 개수를 세는 변수들
        int countVertical = 1;
        int countHorizontal = 1;
        int countDiagonal1 = 1;
        int countDiagonal2 = 1;

        string[,] board = DataManager.Instance.tiledata.stoneTile;
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
            GameEnd();
        }

        
    }

}
