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

        // 렌주(삼삼, 사사 등) 승리 조건 확인
        bool isRenjuWin = player == "P1" && (countVertical > 5 || countHorizontal > 5 || countDiagonal1 > 5 || countDiagonal2 > 5 || IsDoubleThree(x, y) || IsDoubleFour(x, y));

        // 일반 승리 조건 확인
        if ((countVertical >= 5 || countHorizontal >= 5 || countDiagonal1 >= 5 || countDiagonal2 >= 5) && !isRenjuWin)
        {
            GameManager.Instance.GameEnd();
        }
    }
    /// <summary>
    /// 특정 플레이어가 착수할 수 없는 위치 목록을 반환하는 코루틴
    /// </summary>
    /// <param name="player">플레이어 이름</param>
    /// <returns>착수할 수 없는 위치 목록</returns>
    public IEnumerator GetInvalidMovesCoroutine(string player, System.Action<List<Vector2Int>> callback)
    {
        print("GetInvalidMovesCoroutine 실행");
        List<Vector2Int> invalidMoves = new List<Vector2Int>();
        int boardSize = 11;
        string[,] board = DataManager.Instance.tiledata.stoneStatus;

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                if (board[x, y] == "N")
                {
                    board[x, y] = player; // 가상의 돌을 놓습니다.
                    DataManager.Instance.tiledata.stoneStatus = board;
                    if (IsDoubleThree(x, y) || IsDoubleFour(x, y))
                    {
                        invalidMoves.Add(new Vector2Int(x, y));
                        print("33이 발생했습니다 혹은 44가 발생했습니다.");
                    }
                    board[x, y] = "N"; // 가상의 돌을 제거합니다.
                    DataManager.Instance.tiledata.stoneStatus = board;
                }

                // 코루틴이 한 프레임에서 너무 많은 작업을 하지 않도록 중간에 실행을 양보합니다.
                yield return null;
            }
        }

        // 코루틴이 완료된 후 결과를 콜백으로 반환합니다.
        callback(invalidMoves);
    }

    /// <summary>
    /// 삼삼(이중 삼) 여부를 확인
    /// </summary>
    /// <param name="x">돌의 x좌표</param>
    /// <param name="y">돌의 y좌표</param>
    /// <returns>삼삼 여부</returns>
    private bool IsDoubleThree(int x, int y)
    {
        string player = DataManager.Instance.tiledata.stoneStatus[x, y];
        int doubleThreeCount = 0;

        if (CheckOpenThree(x, y, 1, 0, player)) doubleThreeCount++;
        if (CheckOpenThree(x, y, 0, 1, player)) doubleThreeCount++;
        if (CheckOpenThree(x, y, 1, 1, player)) doubleThreeCount++;
        if (CheckOpenThree(x, y, 1, -1, player)) doubleThreeCount++;

        return doubleThreeCount >= 2;
    }

    /// <summary>
    /// 열린 삼(열린 세 개의 돌) 여부를 확인
    /// </summary>
    /// <param name="x">돌의 x좌표</param>
    /// <param name="y">돌의 y좌표</param>
    /// <param name="dx">x방향 이동 값</param>
    /// <param name="dy">y방향 이동 값</param>
    /// <param name="player">플레이어 이름</param>
    /// <returns>열린 삼 여부</returns>
    private bool CheckOpenThree(int x, int y, int dx, int dy, string player)
    {
        int boardSize = 11;
        string[,] board = DataManager.Instance.tiledata.stoneStatus;
        int count = 0;

        // 한쪽 방향으로 세 개의 돌을 확인
        for (int i = 1; i <= 3; i++)
        {
            int nx = x + i * dx;
            int ny = y + i * dy;
            if (nx < 0 || ny < 0 || nx >= boardSize || ny >= boardSize || board[nx, ny] != player)
                break;
            count++;
        }

        // 반대 방향으로 세 개의 돌을 확인
        for (int i = 1; i <= 3; i++)
        {
            int nx = x - i * dx;
            int ny = y - i * dy;
            if (nx < 0 || ny < 0 || nx >= boardSize || ny >= boardSize || board[nx, ny] != player)
                break;
            count++;
        }

        // 열린 형태인지 확인
        bool isOpen = IsEmpty(x + (count + 1) * dx, y + (count + 1) * dy) && IsEmpty(x - (count + 1) * dx, y - (count + 1) * dy);

        return count == 2 && isOpen;
    }

    /// <summary>
    /// 사사(이중 사) 여부를 확인
    /// </summary>
    /// <param name="x">돌의 x좌표</param>
    /// <param name="y">돌의 y좌표</param>
    /// <returns>사사 여부</returns>
    private bool IsDoubleFour(int x, int y)
    {
        string player = DataManager.Instance.tiledata.stoneStatus[x, y];
        int doubleFourCount = 0;

        if (CheckOpenFour(x, y, 1, 0, player)) doubleFourCount++;
        if (CheckOpenFour(x, y, 0, 1 , player)) doubleFourCount++;
        if (CheckOpenFour(x, y, 1, 1,player)) doubleFourCount++;
        if (CheckOpenFour(x, y, 1, -1, player)) doubleFourCount++;

        return doubleFourCount >= 2;
    }

    /// <summary>
    /// 열린 사(열린 네 개의 돌) 여부를 확인
    /// </summary>
    /// <param name="x">돌의 x좌표</param>
    /// <param name="y">돌의 y좌표</param>
    /// <param name="dx">x방향 이동 값</param>
    /// <param name="dy">y방향 이동 값</param>
    /// <param name="player">플레이어 이름</param>
    /// <returns>열린 사 여부</returns>
    private bool CheckOpenFour(int x, int y, int dx, int dy, string player)
    {
        int boardSize = 11;
        string[,] board = DataManager.Instance.tiledata.stoneStatus;
        int count = 0;

        // 한쪽 방향으로 네 개의 돌을 확인
        for (int i = 1; i <= 4; i++)
        {
            int nx = x + i * dx;
            int ny = y + i * dy;
            if (nx < 0 || ny < 0 || nx >= boardSize || ny >= boardSize || board[nx, ny] != player)
                break;
            count++;
        }

        // 반대 방향으로 네 개의 돌을 확인
        for (int i = 1; i <= 4; i++)
        {
            int nx = x - i * dx;
            int ny = y - i * dy;
            if (nx < 0 || ny < 0 || nx >= boardSize || ny >= boardSize || board[nx, ny] != player)
                break;
            count++;
        }

        // 열린 형태인지 확인
        bool isOpen = IsEmpty(x + (count + 1) * dx, y + (count + 1) * dy) && IsEmpty(x - (count + 1) * dx, y - (count + 1) * dy);

        return count == 3 && isOpen;
    }

    /// <summary>
    /// 해당 좌표가 빈 칸인지 확인
    /// </summary>
    /// <param name="x">x좌표</param>
    /// <param name="y">y좌표</param>
    /// <returns>빈 칸 여부</returns>
    private bool IsEmpty(int x, int y)
    {
        int boardSize = 11;
        if (x < 0 || y < 0 || x >= boardSize || y >= boardSize) return false;
        return DataManager.Instance.tiledata.stoneStatus[x, y] == "N";
    }

}
