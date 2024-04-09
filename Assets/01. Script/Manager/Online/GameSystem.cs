using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : Singleton<GameSystem>
{
    private void Update()
    {
        if(DataManager.Instance.gamedata.isGameStart == true)
        {
            TileManager.Instance.OnClickPosition();
        }
    }
    public void ChageTurn()
    {

    }

    public IEnumerator CheckWinCondition(string player, int x, int y)
    {
        yield return null;
        // �������� ũ��
        int boardSize = 11;

        // �� ���⺰�� ���ӵ� ���� ������ ���� ������
        int countVertical = 1;
        int countHorizontal = 1;
        int countDiagonal1 = 1;
        int countDiagonal2 = 1;

        string[,] board = DataManager.Instance.tiledata.stoneTile;
        // ���� ���� Ȯ��
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

        // ���� ���� Ȯ��
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

        // �밢�� ���� Ȯ�� (���� �Ʒ���)
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

        // �밢�� ���� Ȯ�� (���� ����)
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

        // ��� �� �������� 5�� �̻��� ���� ���ӵǾ� ������ �¸�
        if (countVertical >= 5 || countHorizontal >= 5 || countDiagonal1 >= 5 || countDiagonal2 >= 5)
        {
            GameManager.Instance.GameEnd();
        }


    }
}